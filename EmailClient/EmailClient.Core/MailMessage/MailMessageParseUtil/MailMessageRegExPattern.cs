namespace EmailClient.Core.MailMessage.MailMessageParseUtil
{
    public static class MailMessageRegExPattern
    {
        public const string FromWithOutEncoding = @"From: (("")?(?<name>[^"".]+)("")?(?s:.)*\<(?<email>.+)\>|(?<name>)(\<)?(?<email>[^>.]+)(\>)?)";

        //public const string FromWithEncoding =
        //    @"From: =\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q))\?(?<name>[A-Za-z0-9\s]+)\?= \<(?<email>[A-Za-z0-9@.]+)\>";

        public const string FromWithEncoding =
            @"From: =\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q|b|q))\?(?<name>.+)\?=(?s:.)*\<(?<email>.+)\>";

        public const string SubjectWithOutEncoding = @"Subject: (?<subject>.+)";

        public const string SubjectWithEncoding =
            @"Subject: =\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q|b|q))\?(?<subject>.+)\?=";

        public const string Date = @"Date: (?<date>[A-Za-z0-9,+:\s]+) (\+|-)[0-9]{4}";
    }
}
