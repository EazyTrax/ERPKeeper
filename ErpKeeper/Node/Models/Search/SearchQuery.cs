using System;


namespace ERPKeeper.Node.Models.Search
{
    public class SearchModel
    {
        public string FullSearchString { get; set; }


        public string ObjectTypeString { get; set; }
        public Accounting.Enums.ERPObjectType? ObjectType { get; set; }

        public string GroupString { get; set; }
        public string NumberString { get; set; }

        public SearchModel(string searchStr)
        {
            FullSearchString = searchStr.Trim();
            try
            {
                var splitStr = FullSearchString.Split(' ');
                ObjectTypeString = splitStr[0];
                ObjectType = Node.Models.Helpers.ObjectsHelper.LookUp(ObjectTypeString);

                if (splitStr.Length > 1)
                {
                    splitStr = splitStr[1].Split('/');
                    GroupString = splitStr[0];
                    if (splitStr.Length > 1)
                        NumberString = splitStr[1];
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
