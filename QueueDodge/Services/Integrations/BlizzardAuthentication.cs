using System;
using System.Collections.Generic;
using System.Net.Http;

namespace QueueDodge.Integrations
{
    public class BlizzardAuthentication
    {
        private HttpClient http;
        private Uri authorizationUri;
        private Uri tokenUri;
        private Uri checkTokenUri;

        public BlizzardAuthentication(BattleDotSwag.Region region)
        {
            this.http = new HttpClient();
            this.authorizationUri = GetAuthorizationUri(region);
            this.tokenUri = GetTokenUri(region);
            this.checkTokenUri = GetCheckTokenUri(region);
        }

        private Uri GetAuthorizationUri(BattleDotSwag.Region region)
        {
            switch (region)
            {
                case BattleDotSwag.Region.us:
                case BattleDotSwag.Region.eu:
                case BattleDotSwag.Region.kr:
                case BattleDotSwag.Region.tw:
                    return new Uri("https://" + region.ToString() + ".battle.net/oauth/authorize");
                case BattleDotSwag.Region.cn:
                    return new Uri("https://www.battlenet.com.cn/oauth/authorize");
                default:
                    throw new NotSupportedException("This region is not supported yet.");
            }
        }
        private Uri GetTokenUri(BattleDotSwag.Region region)
        {
            switch (region)
            {
                case BattleDotSwag.Region.us:
                case BattleDotSwag.Region.eu:
                case BattleDotSwag.Region.kr:
                case BattleDotSwag.Region.tw:
                    return new Uri("https://" + region.ToString() + ".battle.net/oauth/token");
                case BattleDotSwag.Region.cn:
                    return new Uri("https://www.battlenet.com.cn/oauth/token");
                default:
                    throw new NotSupportedException("This region is not supported yet.");
            }
        }       
        private Uri GetCheckTokenUri(BattleDotSwag.Region region)
        {
            switch (region)
            {
                case BattleDotSwag.Region.us:
                case BattleDotSwag.Region.eu:
                case BattleDotSwag.Region.kr:
                case BattleDotSwag.Region.tw:
                case BattleDotSwag.Region.cn:
                    return new Uri("https://" + region.ToString() + ".battle.net/oauth/check_token");
                default:
                    throw new NotSupportedException("This region is not supported yet.");
            }
        }

        public void GetAuthorization(string clientID, IEnumerable<string> scope, Uri redirectUri)
        {

        }

        public bool CheckToken(string token)
        {
            return false;
        }

        public void RequestAccessTokens()
        {

        }
    }
}
