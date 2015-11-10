namespace EmailClient.Core.MailMessage.MailMessageParseUtil
{
    public static class MailMessageRegExPattern
    {
        public const string FromWithOutEncoding = @"From: (?<name>[A-Za-z0-9\s]+) \<(?<email>[A-Za-z0-9@.]+)\>";

        public const string FromWithEncoding =
            @"From: =\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q))\?(?<name>[A-Za-z0-9\s]+)\?= \<(?<email>[A-Za-z0-9@.]+)\>";

        public const string SubjectWithOutEncoding = @"Subject: (?<subject>[A-Za-z0-9\s=]+)";

        public const string SubjectWithEncoding =
            @"Subject: =\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q))\?(?<subject>[A-Za-z0-9=\s]+)\?=";

        public const string Date = @"Date: (?<date>[A-Za-z0-9,+:\s]+)";
    }
}
