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
using Siege.Requisitions.InternalStorage;
using Siege.Requisitions.Registrations;
using Siege.Requisitions.RegistrationTemplates;

namespace Siege.Requisitions.Extensions.ConditionalAwareness
{
    public class ContextualRegistration<T> : TypedRegistration
    {
        private readonly Func<T> func;

        public ContextualRegistration(Func<T> func) : base(typeof(T))
        {
            this.func = func;
        }

        public override bool IsValid(IServiceLocatorStore context)
        {
            return false;
        }

        public override object GetMappedTo()
        {
            return this.func;
        }

        public override IRegistrationTemplate GetRegistrationTemplate()
        {
            return StaticRegistrationTemplates.ContextualRegistrationTemplate;
        }
    }
}
