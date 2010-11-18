﻿using System;
using System.Web.Security;
using Courier.Sample.Messages;
using CourierApp.Services;
using Siege.Courier;
using Siege.Courier.Subscribers;

namespace CourierApp.Subscribers
{
    public class AccountSubscriber : Subscriber.For<LogOnAccountMessage>,
                                     Subscriber.For<RegisterAccountMessage>
    {
        private readonly IMembershipService membershipService;
        private readonly Func<IServiceBus> serviceBus;

        public AccountSubscriber(IMembershipService membershipService, Func<IServiceBus> serviceBus)
        {
            this.membershipService = membershipService;
            this.serviceBus = serviceBus;
        }

        public void Receive(LogOnAccountMessage message)
        {
            if (membershipService.ValidateUser(message.UserName, message.Password))
            {
                serviceBus().Publish(new MemberAuthenticatedMessage { UserName = message.UserName, RememberMe = message.RememberMe });
                return;
            }

            serviceBus().Publish(new MemberFailedAuthenticationMessage());
        }

        public void Receive(RegisterAccountMessage message)
        {
            MembershipCreateStatus createStatus = membershipService.CreateUser(message.UserName, message.Password, message.Email);

            if (createStatus == MembershipCreateStatus.Success)
            {
                serviceBus().Publish(new RegistrationSucceededMessage { UserName = message.UserName });
                return;
            }

            serviceBus().Publish(new RegistrationFailedMessage { Status = createStatus });
        }
    }
}