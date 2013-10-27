using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPop.Common.Logging;
using OpenPop.Mime;
using OpenPop.Mime.Decode;
using OpenPop.Mime.Header;
using OpenPop.Pop3;

namespace POPMail
{
    public class MailMessage
    {
        private MessageHeader mailHeader;
        private List<MessageHeader> mailHeaders;
        private int mailSize;
        private int mailId;

        public MailMessage(List<MessageHeader> MailHeaders)
        {
            this.mailHeaders = MailHeaders;
        }
    }
}