using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Contract.Constant
{
    public static class MessageError
    {
        public const string NotEmpty = ("Vui lòng kiểm tra lại, bạn nhập thiếu {0}");

        public const string NotEmptyApp = ("Ứng dụng {0} không tồn tại trong hệ thống, vui lòng kiểm tra lại.");

        public const string Message_SSO = ("Vui lòng đăng nhập vào hệ thống VTC SSO");


    }
}
