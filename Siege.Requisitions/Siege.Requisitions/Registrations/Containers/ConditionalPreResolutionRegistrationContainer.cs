using System;
using System.Collections.Generic;

namespace Siege.Requisitions.Registrations.Containers
{
    public class ConditionalPreResolutionRegistrationContainer : IRegistrationContainer
    {
        private readonly CompositeRegistrationList registrations = new CompositeRegistrationList();

        public void Add(IRegistration registration)
        {
            registrations.Add(registration.GetMappedToType(), registration);
        }

        public List<IRegistration> GetRegistrationsForType(Type type)
        {
            return registrations.GetregistrationsForType(type);
        }

        public bool Contains(Type type)
        {
            return registrations.Contains(type);
        }
    }
}