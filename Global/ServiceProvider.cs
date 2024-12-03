using System;
using System.Collections.Generic;

namespace SpellingRace.Global
{
    // JPWP: Klasa służąca jako kontener dla dependency injection, pozwalająca na rejestrację i dostęp do instancji zarejestrowanych klas w środku innych klas 
    // (aby nie tworzyć nowych instancji czy przesyłać ich przez konstruktory) 
    public static class ServiceProvider {
        private static readonly Dictionary<Type, object> _services = new();

        public static void Register<T>(T service) where T : class
        {
            _services[typeof(T)] = service;
        }

        public static T Resolve<T>() where T : class
        {
            return _services[typeof(T)] as T;
        }
    }
}