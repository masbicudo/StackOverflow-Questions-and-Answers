using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using HttpFileServer;

public class Index : HttpRequestHandler
{
    public override async Task RespondAsync(HttpContext context)
    {
        var str = "\"test\"  \"--test\" \"test\" \"--test\" \"test\" \"--test\" \"test\" \"--test\" \"test\" \"--test\" \"test\" \"--test\" \"--test\" \"test\" \"--test\" \"test\" \"--test\" \"test\"\"--test\" \"--test\" \"--test\" \"--test\" \"--test\" \"test\" \"--test\" \"--test\" \" test \"";
        var matches = Regex.Matches("\"((\\.|[^\"])*)\"", str);
        //var arr = matches.Select(x => x[1].Value).ToArray();
        //String.Join(arr, "<br />");
        var msg = "xpto";

        using (var writer = new StreamWriter(context.Output, Encoding.ASCII))
        {
            // writing header
            await writer.WriteHttpHeaderAsync("HTTP/1.1 200 OK");
            await writer.WriteHttpHeaderAsync("Date", DateTime.UtcNow.ToString("R"));
            await writer.WriteHttpHeaderAsync("Server", "Mini-Http-Server");
            await writer.WriteHttpHeaderAsync("Accept-Ranges", "bytes");
            await writer.WriteHttpHeaderAsync("Content-Length", "" + msg.Length);
            await writer.WriteHttpHeaderAsync("Content-Type", "application/json");
            await writer.WriteHttpHeaderAsync("Last-Modified", DateTime.UtcNow.ToString("R"));
            await writer.WriteHttpHeaderAsync("");

            // writing response body
            await writer.WriteLineAsync(msg);
        }

        context.Handled();
    }
}