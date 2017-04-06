using System;
using System.Collections.Generic;
using System.Linq;

namespace TOF.Core.DependencyInjection
{
    public class RegistrationManager
    {
        private IDictionary<Type, Registration> _mapRegistrations = null;

        public RegistrationManager(IDictionary<Type, Registration> MapRegistrations)
        {
            _mapRegistrations = MapRegistrations;
        }

        public Registration Find(Type FindType)
        {
            if (FindType.IsInterface)
                return FindInterface(FindType);
            else
                return FindClass(FindType);
        }

        public Registration FindInterface(Type FindInterfaceType)
        {
            if (!FindInterfaceType.IsInterface)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes.Where(t => t.IsInterface && t.FullName == FindInterfaceType.FullName).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindClass(Type FindClassType)
        {
            if (!FindClassType.IsClass)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes.Where(t => t == FindClassType).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindGeneric(Type FindType)
        {
            if (FindType.IsInterface)
                return FindGenericInterface(FindType);
            else
                return FindGenericClass(FindType);
        }

        public Registration FindGenericInterface(Type FindInterfaceType)
        {
            if (!FindInterfaceType.IsInterface)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes
                .Where(t =>  t == FindInterfaceType && t.IsGenericType).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindGenericClass(Type FindClassType)
        {
            if (!FindClassType.IsClass)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes.Where(t => t == FindClassType && t.IsGenericType).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindGenericDefinition(Type FindType)
        {
            if (FindType.IsInterface)
                return FindGenericInterfaceDefinition(FindType);
            else
                return FindGenericClassDefinition(FindType);
        }

        public Registration FindGenericInterfaceDefinition(Type FindInterfaceType)
        {
            if (!FindInterfaceType.IsInterface)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes
                .Where(t => t == FindInterfaceType && t.IsGenericTypeDefinition).Any());

            if (!query.Any())
                return null;

            if (query.Count() > 1)
            {
                if (query.Where(t => t.Value.IsDefault).Any())
                    return query.Where(t => t.Value.IsDefault).First().Value;
            }

            return query.First().Value;
        }

        public Registration FindGenericClassDefinition(Type FindClassType)
        {
            if (!FindClassType.IsClass)
                throw new ArgumentException("ERROR_TYPE_STYLE_MISMATCH");

            var query = _mapRegistrations.Where(r => r.Value.MapTypes
                .Where(t => t == FindClassType && t.IsGenericTypeDefinition).Any());

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
