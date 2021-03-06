/*   Copyright 2009 - 2010 Marcus Bratton

     Licensed under the Apache License, Version 2.0 (the "License");
     you may not use this file except in compliance with the License.
     You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

     Unless required by applicable law or agreed to in writing, software
     distributed under the License is distributed on an "AS IS" BASIS,
     WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     See the License for the specific language governing permissions and
     limitations under the License.
*/

using System;
using Siege.ServiceLocator.Extensions.Decorator;
using Siege.ServiceLocator.Extensions.FactorySupport;
using Siege.ServiceLocator.Extensions.Initialization;
using Siege.ServiceLocator.Registrations;

namespace Siege.ServiceLocator.Extensions.ExtendedRegistrationSyntax
{
    public abstract class ActivationRule<TBaseService> : ResolutionRules.ConditionalActivationRule<TBaseService>
    {
        public IRegistration ConstructWith<TService>(Func<IInstanceResolver, TService> factoryMethod)
        {
            var registration = new ConditionalFactoryRegistration<TService>();

            registration.MapsTo<TService>();
            registration.SetActivationRule(this);
            registration.ConstructWith(factoryMethod);

            return registration;
        }

        public IRegistration InitializeWith(Action<TBaseService> action)
        {
            var registration = new ConditionalInitializationRegistration<TBaseService>();

            registration.MapsTo<TBaseService>();
            registration.SetActivationRule(this);

            Func<TBaseService, TBaseService> func = service =>
                                                        {
                                                            action(service);
                                                            return service;
                                                        };

            registration.Associate(func);

            return registration;
        }

        public IRegistration DecorateWith(Func<TBaseService, TBaseService> func)
        {
            var registration = new ConditionalDecoratorRegistration<TBaseService>();

            registration.MapsTo<TBaseService>();
            registration.SetActivationRule(this);

            registration.Associate(func);

            return registration;
        }
    }
}