using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace studio
{
    enum CommandID
    {
        Command_Shader_Build,
        Command_Shader_Uniform_Var,
    };

    public abstract class Command
    {
        public abstract string genCommandString();
        

        protected Command()
        {
            JObject args = new JObject();
            JObject result = new JObject();
            root.Add("id", 0);
            root.Add("args", args);
            root.Add("result", result);
        }
        protected JObject root = new JObject();
    }



    public class BuildProgramCommand : Command
    {
        public override string genCommandString()
        {
            root["id"] = (int)CommandID.Command_Shader_Build;
            JObject args = root["args"] as JObject;
            args.Add("");
            return "";
        }
    }

    class CommandFactory
    {
        public Command createCommand(CommandID id)
        {
            switch(id)
            {
                case CommandID.Command_Shader_Build:    return new BuildProgramCommand();
                default: return null;
            }
        }
    }
}
