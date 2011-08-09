using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsAero.Design
{
    internal delegate T Func<T>();

    internal interface IDesignerUtilsClient
    {
        IComponent Component { get; }
        object GetService(Type type);
    }

    internal sealed class DesignerUtils
    {
        private readonly IDesignerUtilsClient _owner;

        private IDesignerHost _host;
        private ISelectionService _selection;
        private IComponentChangeService _changes;

        public DesignerUtils(IDesignerUtilsClient owner)
        {
            _owner = owner;
        }

        public IDesignerHost DesignerHost
        {
            get
            {
                if (_host == null)
                {
                    _host = GetService<IDesignerHost>();
                }

                return _host;
            }
        }

        public ISelectionService SelectionService
        {
            get
            {
                if (_selection == null)
                {
                    _selection = GetService<ISelectionService>();
                }

                return _selection;
            }
        }

        public IComponentChangeService ChangeService
        {
            get
            {
                if (_changes == null)
                {
                    _changes = GetService<IComponentChangeService>();
                }

                return _changes;
            }
        }

        public TService GetService<TService>() where TService : class
        {
            TService result = _owner.GetService(typeof(TService)) as TService;

            if (result == null)
            {
                throw new InvalidOperationException("GetService " + typeof(TService).Name + " failed.");
            }

            return result;
        }

        public TComponent CreateComponent<TComponent>() where TComponent : IComponent
        {
            return (TComponent)DesignerHost.CreateComponent(typeof(TComponent));
        }

        private DesignerTransaction CreateDesignerTransaction(String description)
        {
            try
            {
                return DesignerHost.CreateTransaction(description);
            }
            catch (CheckoutException exc)
            {
                if (exc != CheckoutException.Canceled)
                {
                    throw;
                }
            }

            return null;
        }

        public T ExecuteWithTransaction<T>(String description, Func<T> func)
        {
            using (var tx = CreateDesignerTransaction(description + ' ' + Component.Site.Name))
            {
                if (tx != null)
                {
                    try
                    {
                        T result = func();
                        tx.Commit();

                        return result;
                    }
                    catch
                    {
                        tx.Cancel();
                        throw;
                    }
                }
            }

            return default(T);
        }

        public void ExecuteWithTransaction(String description, MethodInvoker method)
        {
            ExecuteWithTransaction<Object>(description, delegate
            {
                method();
                return null;
            });
        }

        private IComponent Component
        {
            get { return _owner.Component; }
        }

        public object GetPropertyValue(string name)
        {
            return GetPropertyValue(Component, name);
        }

        public object GetPropertyValue(PropertyDescriptor descriptor)
        {
            return GetPropertyValue(Component, descriptor);
        }

        public object GetPropertyValue(object target, string name)
        {
            var descriptor = GetProperty(name);

            if (descriptor != null)
            {
                return GetPropertyValue(Component, descriptor);
            }

            return null;
        }

        public static object GetPropertyValue(object target, PropertyDescriptor descriptor)
        {
            return descriptor.GetValue(target);
        }

        public T GetPropertyValue<T>(string name)
        {
            return GetPropertyValue<T>(Component, name);
        }

        public static T GetPropertyValue<T>(object target, string name)
        {
            var descriptor = GetProperty(target, name);
            
            if (descriptor != null)
            {
                return GetPropertyValue<T>(target, descriptor);
            }

            return default(T);
        }

        public static T GetPropertyValue<T>(object target, PropertyDescriptor descriptor)
        {
            var value = GetPropertyValue(target, descriptor);

            if (value is T)
            {
                return (T)(value);
            }

            return default(T);
        }

        public void SetPropertyValueWithNotification(object target, PropertyDescriptor descriptor, object value)
        {
            ChangeService.OnComponentChanging(target, descriptor);
            SetPropertyValue(target, descriptor, value);
            ChangeService.OnComponentChanged(target, descriptor, null, null);
        }

        public void SetPropertyValueWithNotification(PropertyDescriptor descriptor, object value)
        {
            SetPropertyValueWithNotification(Component, descriptor, value);
        }

        public void SetPropertyValueWithNotification(object target, string name, object value)
        {
            var descriptor = GetProperty(name);

            if (descriptor != null)
            {
                SetPropertyValueWithNotification(target, descriptor, value);
            }
        }

        public void SetPropertyValueWithNotification(string name, object value)
        {
            SetPropertyValueWithNotification(Component, name, value);
        }

        public void SetPropertyValue(string name, object value)
        {
            SetPropertyValue(Component, name, value);
        }

        public void SetPropertyValue(PropertyDescriptor descriptor, object value)
        {
            SetPropertyValue(Component, descriptor, value);
        }

        public void SetPropertyValue(object target, string name, object value)
        {
            var descriptor = GetProperty(target, name);

            if (descriptor != null)
            {
                SetPropertyValue(target, descriptor, value);
            }
        }

        public void SetPropertyValue(object target, PropertyDescriptor property, object value)
        {
            property.SetValue(target, value);
        }

        public PropertyDescriptor GetProperty(string name)
        {
            return GetProperty(_owner.Component, name);
        }

        public static PropertyDescriptor GetProperty(object target, string name)
        {
            return TypeDescriptor.GetProperties(target)[name];
        }
    }
}
