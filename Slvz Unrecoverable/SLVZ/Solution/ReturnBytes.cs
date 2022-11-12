namespace Slvz_Unrecoverable.SLVZ.Solution;

public class ReturnBytes
{
    public static byte[] GetBytes(int? times = 1)
    {

        Stream stream = Application.Context.Assets.Open("Trash.slvz");

        MemoryStream memory = new MemoryStream();

        for (int i = 0; i <= times; i++)
            stream.CopyTo(memory);

        stream.Close();

        return memory.ToArray();
    }
}
