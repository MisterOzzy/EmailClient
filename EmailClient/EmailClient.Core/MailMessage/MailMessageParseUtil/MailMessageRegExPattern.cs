namespace EmailClient.Core.MailMessage.MailMessageParseUtil
{
    public static class MailMessageRegExPattern
    {
        public const string FromWithOutEncoding =
            @"From: (("")?(?<name>[^"".]+)("")?(?s:.)*\<(?<email>.+)\>|(?<name>)(\<)?(?<email>[^>.]+)(\>)?)";
        
        public const string EncodedString =
            @"(=\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q|b|q))\?(?<data>[\S.]+)\?=)+";

        public const string Date = @"Date: (?<date>[A-Za-z0-9,+:\s]+) (\+|-)[0-9]{4}";

        public const string SubjectWithOutEncoding = @"Subject: (?<subject>.+)";

        public const string FromWithEncoding =
            @"From: (?<name>(=\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q|b|q))\?.+\?=(?s:.)*)+)\<(?<email>.+)\>";

        public const string SubjectWithEncoding =
            @"Subject: (?<subject>(=\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q|b|q))\?.*\?=[\s]*)+)";
    }
}
