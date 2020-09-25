using System;
using System.Collections.Generic;
using UnityEngine;
using SA.Foundation.Time;
using SA.Foundation.Templates;

namespace SA.CrossPlatform.InApp
{
    [Serializable]
    abstract class UM_AbstractTransaction<T>
    {
        [SerializeField]
        protected string m_id;
        [SerializeField]
        protected string m_productId;
        [SerializeField]
        protected long m_unixTimestamp;

        [SerializeField]
        protected UM_TransactionState m_state;
        [SerializeField]
        protected SA_Error m_error = null;

        public object NativeTemplate { get; private set; }

        protected void SetNativeTransaction(T nativeTemplate)
        {
            NativeTemplate = nativeTemplate;
        }

        public string Id => m_id;

        public string ProductId => m_productId;

        public DateTime Timestamp
        {
            get
            {
                var timestamp = DateTime.MinValue;
                try
                {
                    timestamp = SA_Unix_Time.ToDateTime(m_unixTimestamp);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed to convert UNIX " + m_unixTimestamp + " time to DateTime: " + ex.Message);
                }

                return timestamp;
            }
        }

        public SA_Error Error => m_error;

        public UM_TransactionState State => m_state;
    }
}
