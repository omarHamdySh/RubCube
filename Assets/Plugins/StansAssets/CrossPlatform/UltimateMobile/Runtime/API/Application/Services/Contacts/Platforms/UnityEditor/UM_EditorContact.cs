using System;
using UnityEngine;

namespace SA.CrossPlatform.App
{
    [Serializable]
    class UM_EditorContact : UM_AbstractContact, UM_iContact
    {
        public UM_EditorContact(string name, string phone, string email)
        {
            m_name = name;
            m_phone = phone;
            m_email = email;
        }

        public new string Name
        {
            get => m_name;
            set => m_name = value;
        }

        public new string Phone
        {
            get => m_phone;
            set => m_phone = value;
        }

        public new string Email
        {
            get => m_email;
            set => m_email = value;
        }

        public UM_EditorContact Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<UM_EditorContact>(json);
        }
    }
}
