
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPKeeperCore.Enterprise.Models.Enums;

namespace ERPKeeperCore.Enterprise.Helpers
{
    using System;

    public static class ThaiMoneyConverter
    {
        private static readonly string[] Units = { "", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า" };
        private static readonly string[] Tens = { "", "สิบ", "ยี่สิบ", "สามสิบ", "สี่สิบ", "ห้าสิบ", "หกสิบ", "เจ็ดสิบ", "แปดสิบ", "เก้าสิบ" };

        public static string ReturnThaiMoney(decimal amount)
        {
            if (amount == 0) return "ศูนย์บาทถ้วน";

            var baht = (int)amount;
            var satang = (int)((amount - baht) * 100);

            var bahtText = ConvertToThai(baht);
            var satangText = satang > 0 ? ConvertToThai(satang) : "";

            var result = bahtText + "บาท";
            result += satang > 0 ? satangText + "สตางค์" : "ถ้วน";

            return result;
        }

        private static string ConvertToThai(int number)
        {
            if (number == 0) return "";

            var result = "";

            if (number >= 1000000)
            {
                result += ConvertToThai(number / 1000000) + "ล้าน";
                number %= 1000000;
            }

            if (number >= 100000)
            {
                result += ConvertToThai(number / 100000) + "แสน";
                number %= 100000;
            }

            if (number >= 10000)
            {
                result += ConvertToThai(number / 10000) + "หมื่น";
                number %= 10000;
            }

            if (number >= 1000)
            {
                result += ConvertToThai(number / 1000) + "พัน";
                number %= 1000;
            }

            if (number >= 100)
            {
                result += ConvertToThai(number / 100) + "ร้อย";
                number %= 100;
            }

            if (number >= 10)
            {
                result += Tens[number / 10];
                number %= 10;
            }

            if (number > 0)
            {
                if (number == 1 && result != "" && !result.EndsWith("สิบ"))
                {
                    result += "เอ็ด";
                }
                else
                {
                    result += Units[number];
                }
            }

            return result;
        }
    }

}
