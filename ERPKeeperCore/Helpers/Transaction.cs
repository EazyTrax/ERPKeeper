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
            // Round to 2 decimal places
            amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

            if (amount == 0) return "ศูนย์บาทถ้วน";

            // Separate baht and satang
            var baht = (int)amount; // Integer part
            var satang = (int)((amount - baht) * 100); // Fractional part (satang)

            // Convert both parts
            var bahtText = ConvertToThai(baht);
            var satangText = satang > 0 ? ConvertToThai(satang) : "";

            // Combine result
            var result = bahtText + "บาท";
            result += satang > 0 ? satangText + "สตางค์" : "ถ้วน";

            return result;
        }

        private static string ConvertToThai(int number)
        {
            if (number == 0) return "";

            var result = "";

            // Millions and beyond
            if (number >= 1000000)
            {
                var millionPart = number / 1000000;
                result += ConvertToThai(millionPart) + "ล้าน";
                number %= 1000000; // Remaining part
            }

            // Hundred-thousand (แสน)
            if (number >= 100000)
            {
                var hundredThousandPart = number / 100000;
                result += Units[hundredThousandPart] + "แสน";
                number %= 100000;
            }

            // Ten-thousand (หมื่น)
            if (number >= 10000)
            {
                var tenThousandPart = number / 10000;
                if (tenThousandPart == 1)
                    result += "หนึ่งหมื่น";
                else
                    result += Units[tenThousandPart] + "หมื่น";
                number %= 10000;
            }

            // Thousands (พัน)
            if (number >= 1000)
            {
                var thousandPart = number / 1000;
                result += Units[thousandPart] + "พัน";
                number %= 1000;
            }

            // Hundreds (ร้อย)
            if (number >= 100)
            {
                var hundredPart = number / 100;
                result += Units[hundredPart] + "ร้อย";
                number %= 100;
            }

            // Tens (สิบ, ยี่สิบ, สามสิบ)
            if (number >= 10)
            {
                var tenPart = number / 10;
                if (tenPart == 1)
                    result += "สิบ";
                else
                    result += Tens[tenPart];
                number %= 10;
            }

            // Units (เอ็ด for special case)
            if (number > 0)
            {
                if (number == 1 && result != "") // "เอ็ด" only after tens
                    result += "เอ็ด";
                else
                    result += Units[number];
            }

            return result;
        }
    }

}
