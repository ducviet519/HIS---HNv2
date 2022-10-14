using System;
using System.DirectoryServices;
using System.Text;

namespace UserAuthentication
{
    public class ActiveDirectoryValidator
    {
        private string _path;
        private string _filterAttribute;

        public ActiveDirectoryValidator(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string domainName, string userName, string password)
        {
            string domainAndUsername = domainName + @"\" + userName;

            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, password);
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + userName + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                //var groups = GetGroups(search);

                if (null == result)
                {
                    return false;
                }
                // Update the new path to the user in the directory
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Login Error: " + ex.Message);
            }
            return true;
        }

        private string GetGroups(DirectorySearcher search) 
        {
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder(); //stuff them in | delimited

            SearchResult result = search.FindOne();
            int propertyCount = result.Properties["memberOf"].Count;
            String dn;
            int equalsIndex, commaIndex;

            for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
            {
                dn = (String)result.Properties["memberOf"][propertyCounter];

                equalsIndex = dn.IndexOf("=", 1);
                commaIndex = dn.IndexOf(",", 1);
                if (-1 == equalsIndex)
                {
                    return null;
                }
                groupNames.Append(dn.Substring((equalsIndex + 1),(commaIndex - equalsIndex) - 1));
                groupNames.Append("|");
            }

            return groupNames.ToString();
        }
    }
}