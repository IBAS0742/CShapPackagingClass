using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GetAllRequestDLL
{
    public class Server
    {
        public HttpListener listener { get; set; } = null;
        HttpListenerContext ctx;
        int default_port = 8080;
        private ServerDear sd = null;
        public Func<Dictionary<string, string>, ServerDear> fun;
        public Server(Func<Dictionary<string, string>, ServerDear> f)
        {
            fun = f;
        }
        public void Create_Service(int port = -1)
        {
            StringBuilder sb_ip = new StringBuilder();
            if (listener != null)
            {
                //throw new Exception("Server is start");
                Console.WriteLine("[ERROR] Server is Start !");
                return;
            }
            if (port == -1) {
                port = default_port;
            }
            else if (!CheckPort(port))
            {
                //throw new Exception("Port range {1000 ~ 9999}");
                Console.WriteLine("[ERROR] Port range {1000 ~ 9999}");
                return;
            }
            sb_ip.Append("http://localhost:" + port + "/");
            listener = new HttpListener();
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            listener.Prefixes.Add(sb_ip.ToString());
            listener.Start();
            Console.WriteLine("[Info] Server Start !");
        }
        public void Close_Service() {
            if (listener != null)
            {
                listener.Close();
                listener = null;
                Console.WriteLine("[Info] Server is Stop !");
            }
        }
        public bool CheckPort(int port)
        {
            if (port > 999 && port < 10000)
            {
                return true;
            } else {
                return false;
            }
        }
        /// <summary>
        /// only get post data
        /// data format : key=value&key=value&key=value
        /// </summary>
        public Dictionary<string,string> GetMessage()
        {
            Dictionary<string, string> datas = new Dictionary<string, string>();
            ctx = listener.GetContext();
            StreamReader inputStr = new StreamReader(ctx.Request.InputStream);
            string data = inputStr.ReadToEnd();
            string[] lit = data.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0;i < lit.Count();i++)
            {
                string[] lits = lit[i].Split('=');
                datas.Add(lits[0], lits[1]);
            }
            foreach (string key in ctx.Request.QueryString.Keys){
                datas.Add(key, ctx.Request.QueryString[key]);
            }
            return datas;
        }
        public void Response(string repStr)
        {
            ctx.Response.StatusCode = 200;
            using (StreamWriter sw = new StreamWriter(ctx.Response.OutputStream))
            {
                sw.WriteLine(repStr);
            }
        }
        public void Response(object repStr)
        {
            ctx.Response.StatusCode = 200;
            using (StreamWriter sw = new StreamWriter(ctx.Response.OutputStream))
            {
                sw.WriteLine(JsonConvert.SerializeObject(repStr));
            }
        }
        public static ServerDear DearMessage(Dictionary<string,string> dic)
        {
            foreach (string key  in dic.Keys)
            {
                Console.WriteLine(key + "..." + dic[key]);
            }
            return new ServerDear() { isOver = false,data = new { statue = "success", message = "over",time = DateTime.Now.ToLocalTime() } };
        }
        public void Listen()
        {
            bool isOver = false;
            while (!isOver)
            {
                sd = fun(GetMessage());
                isOver = sd.isOver;
                //isOver = DearMessage(GetMessage());
                Response(sd.data);
            }
        }
    }

    public class ServerDear
    {
        public Object data;
        public bool isOver;
    }
}
