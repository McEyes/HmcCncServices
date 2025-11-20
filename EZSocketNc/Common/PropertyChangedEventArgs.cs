using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;

using EZSocketNc.Mqtts.Dtos;

using Newtonsoft.Json;

namespace EZSocketNc.Commons
{
    public class EzPropertyChangedEventArgs : EventArgs
    {
        private readonly string propertyName;
        private object _OldValue;
        private object _NewValue;

        public virtual string PropertyName
        {
            get
            {
                return propertyName;
            }
        }

        public virtual object OldValue
        {
            get
            {
                return _OldValue;
            }
        }
        public virtual object NewValue
        {
            get
            {
                return _NewValue;
            }
        }


        public EzPropertyChangedEventArgs(string propertyName, object oldValue, object newValue)
        {
            this.propertyName = propertyName;
            this._OldValue = oldValue;
            this._NewValue = newValue;
        }
    }
}
