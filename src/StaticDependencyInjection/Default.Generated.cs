using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rock.StaticDependencyInjection
{
    /// <summary>
    /// An internal helper class that makes it easier for your library to implement
    /// the static default pattern that Rock.StaticDependencyInjection is meant to
    /// support.
    /// </summary>
    /// <typeparam name="T">A type that requires a default instance.</typeparam>
    internal sealed class Default<T>
    {
        private readonly Lazy<T> _defaultInstance;
        private Lazy<T> _currentInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Default{T}"/> class.
        /// </summary>
        /// <param name="createDefaultInstance">
        /// A function that describes how to create the the object returned by the
        /// <see cref="DefaultInstance"/> property.
        /// </param>
        public Default(Func<T> createDefaultInstance)
        {
            _defaultInstance = new Lazy<T>(createDefaultInstance);
            _currentInstance = _defaultInstance;
        }

        /// <summary>
        /// Gets the default instance of <typeparamref name="T"/>. This value is 
        /// returned by the <see cref="Current"/> property when neither the
        /// <see cref="SetCurrent(System.Func{T})"/> nor <see cref="SetCurrent(T)"/>
        /// has been called.
        /// </summary>
        public T DefaultInstance
        {
            get { return _defaultInstance.Value; }
        }

        /// <summary>
        /// Gets the current value for an instance of type <typeparamref name="T"/>.
        /// </summary>
        public T Current
        {
            get { return _currentInstance.Value; }
        }

        /// <summary>
        /// Restores the value of the <see cref="Current"/> property to the value of
        /// the <see cref="DefaultInstance"/> property.
        /// </summary>
        public void RestoreDefault()
        {
            SetCurrent(null);
        }

        /// <summary>
        /// Sets the value of the <see cref="Current"/> property. If the
        /// <paramref name="getInstance"/> parameter is null, sets the value of the
        /// <see cref="Current"/> to the value of the <see cref="DefaultInstance"/>
        /// property.
        /// </summary>
        /// <param name="getInstance">
        /// A function that returns the value for the <see cref="Current"/> property.
        /// </param>
        public void SetCurrent(Func<T> getInstance)
        {
            _currentInstance =
                getInstance == null
                    ? _defaultInstance
                    : new Lazy<T>(getInstance);
        }

        /// <summary>
        /// Sets the value of the <see cref="Current"/> property. If the
        /// <paramref name="instance"/> parameter is null, sets the value of the
        /// <see cref="Current"/> to the value of the <see cref="DefaultInstance"/>
        /// property.
        /// </summary>
        /// <param name="instance">
        /// The value for the <see cref="Current"/> property.
        /// </param>
        public void SetCurrent(T instance)
        {
            _currentInstance =
                instance == null
                    ? _defaultInstance
                    : new Lazy<T>(() => instance);
        }
    }
}
