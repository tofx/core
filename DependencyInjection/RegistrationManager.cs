using System;
using System.Collections.Generic;
using System.Linq;

namespace tofx.Core.DependencyInjection
{
    public class RegistrationManager
    {
        private IDictionary<Type, Registration> _mapRegistrations = null;

        public RegistrationManager(IDictionary<Type, Registration> mapRegistrations)
        {
            _mapRegistrations = mapRegistrations;
        }

        public Registration Find(Type findType)
        {
            if (findType.IsInterface)
                return FindInterface(findType);
            else
                return FindClass(findType);
        }

        public Registration FindInterface(Type findInterfaceType)
        {
            if (!findInterfaceType.IsInterface)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes.Where(t => t.IsInterface && t.FullName == findInterfaceType.FullName).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindClass(Type findClassType)
        {
            if (!findClassType.IsClass)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes.Where(t => t == findClassType).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindGeneric(Type findType)
        {
            if (findType.IsInterface)
                return FindGenericInterface(findType);
            else
                return FindGenericClass(findType);
        }

        public Registration FindGenericInterface(Type findInterfaceType)
        {
            if (!findInterfaceType.IsInterface)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes
                .Where(t =>  t == findInterfaceType && t.IsGenericType).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindGenericClass(Type findClassType)
        {
            if (!findClassType.IsClass)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes.Where(t => t == findClassType && t.IsGenericType).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindGenericDefinition(Type findType)
        {
            if (findType.IsInterface)
                return FindGenericInterfaceDefinition(findType);
            else
                return FindGenericClassDefinition(findType);
        }

        public Registration FindGenericInterfaceDefinition(Type findInterfaceType)
        {
            if (!findInterfaceType.IsInterface)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes
                .Where(t => t == findInterfaceType && t.IsGenericTypeDefinition).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindGenericClassDefinition(Type findClassType)
        {
            if (!findClassType.IsClass)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes
                .Where(t => t == findClassType && t.IsGenericTypeDefinition).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }
    }
}
