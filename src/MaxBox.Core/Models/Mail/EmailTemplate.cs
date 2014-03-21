using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Mail;
using System.Text;

namespace MaxBox.Core.Models
{
    public class EmailTemplate : DynamicObject
    {
        private readonly Dictionary<string, string> _placeHolders = new Dictionary<string, string>();

        public int Id { get; set; }

        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string PlaceHoldersString { get; set; }

        public string GenerateEmailBody()
        {
            var output = new StringBuilder(Body);
            foreach (var placeHolder in _placeHolders)
            {
                switch (placeHolder.Key)
                {
                    case "Date":
                        output.Replace("{{" + placeHolder.Key + "}}", DateTime.Now.ToShortDateString());
                        break;
                    case "Time":
                        output.Replace("{{" + placeHolder.Key + "}}", DateTime.Now.ToShortTimeString());
                        break;
                    default:
                        output.Replace("{{" + placeHolder.Key + "}}", placeHolder.Value);
                        break;
                }

                output.Replace("{{" + placeHolder.Key + "}}", placeHolder.Value);
            }
            output.Replace("\n", "<br/>");
            return output.ToString();
        }

        public MailMessage GenerateMailMessage(string from, string to)
        {
            var mailMessage = new MailMessage(from, to, Subject, GenerateEmailBody());
            mailMessage.IsBodyHtml = true;
            return mailMessage;
        }

        public void AddPlaceHolder(string placeHolderName)
        {
            placeHolderName = placeHolderName.Trim();
            _placeHolders.Add(placeHolderName, "");
            PlaceHoldersString = string.Join(",", GetDynamicMemberNames());
        }


        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_placeHolders.Count == 0)
            {
                foreach (string placeholder in PlaceHoldersString.Split(','))
                {
                    _placeHolders.Add(placeholder, "");
                }
            }
            if (!_placeHolders.ContainsKey(binder.Name))
            {
                throw new Exception(binder.Name + " is not valid PlaceHolder on this emailtemplate! (" +
                                    PlaceHoldersString + ")");
            }
            _placeHolders[binder.Name] = (string) value;

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_placeHolders.ContainsKey(binder.Name))
            {
                result = _placeHolders[binder.Name];
                return true;
            }
            return base.TryGetMember(binder, out result);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _placeHolders.Keys;
        }
    }
}