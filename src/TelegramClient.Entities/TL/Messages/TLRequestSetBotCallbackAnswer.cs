using System.IO;

namespace TelegramClient.Entities.TL.Messages
{
    [TlObject(-920136629)]
    public class TlRequestSetBotCallbackAnswer : TlMethod
    {
        public override int Constructor => -920136629;

        public int Flags { get; set; }
        public bool Alert { get; set; }
        public long QueryId { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Alert ? Flags | 2 : Flags & ~2;
            Flags = Message != null ? Flags | 1 : Flags & ~1;
            Flags = Url != null ? Flags | 4 : Flags & ~4;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Alert = (Flags & 2) != 0;
            QueryId = br.ReadInt64();
            if ((Flags & 1) != 0)
                Message = StringUtil.Deserialize(br);
            else
                Message = null;

            if ((Flags & 4) != 0)
                Url = StringUtil.Deserialize(br);
            else
                Url = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            bw.Write(QueryId);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Message, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Url, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}