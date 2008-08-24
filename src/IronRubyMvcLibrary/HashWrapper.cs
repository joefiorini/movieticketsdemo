using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Ruby.Builtins;
using System.Globalization;

namespace IronRubyMvcLibrary {
    public class HashWrapper : CustomTypeDescriptor {

        public HashWrapper(Hash hash) {
            Object = hash;
        }

        public Hash Object {
            get;
            private set;
        }

        public override PropertyDescriptorCollection GetProperties() {
            PropertyDescriptor[] descriptors = Object.Select(
                entry => new HashPropertyDescriptor(Convert.ToString(entry.Key, CultureInfo.InvariantCulture), entry.Value)).ToArray();
            return new PropertyDescriptorCollection(descriptors);
        }

        private class HashPropertyDescriptor : PropertyDescriptor {

            private object _value;

            public HashPropertyDescriptor(string name, object value)
                : base(name, null /* attributes */) {
                _value = value;
            }

            public override bool CanResetValue(object component) {
                return false;
            }

            public override Type ComponentType {
                get { throw new NotImplementedException(); }
            }

            public override object GetValue(object component) {
                return _value;
            }

            public override bool IsReadOnly {
                get { return true; }
            }

            public override Type PropertyType {
                get { return typeof(object); }
            }

            public override void ResetValue(object component) {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value) {
                throw new NotImplementedException();
            }

            public override bool ShouldSerializeValue(object component) {
                throw new NotImplementedException();
            }
        }
    }
}
