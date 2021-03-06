﻿/*   Copyright 2009 - 2010 Marcus Bratton

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
using Siege.ServiceLocator.Extensions.ExtendedRegistrationSyntax;
using Siege.ServiceLocator.RegistrationPolicies;

namespace Siege.ServiceLocator.Extensions.ConditionalAwareness
{
    public class Awareness
    {
        public static Action<IServiceLocator> Of<T>(Func<T> func)
        {
            return serviceLocator =>
            {
                serviceLocator.Store.AddStore<IAwarenessStore>(new AwarenessStore());
                if (!serviceLocator.HasTypeRegistered(typeof(ContextualRegistrationStore))) serviceLocator.Register<Singleton>(Given<ContextualRegistrationStore>.Then<ContextualRegistrationStore>());

                serviceLocator.Register(new ContextualRegistration<T>(func));
            };
        }
    }
}