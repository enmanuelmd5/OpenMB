﻿using AMOFGameEngine.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMOFGameEngine.Script.Command
{
    public class StoreScriptCommand : IScriptCommand
    {
        private ScriptContext context;
        public StoreScriptCommand(ScriptContext context)
        {
            this.context = context;
        }
        public object[] CommandArgs
        {
            get;
        }

        public string CommandName
        {
            get
            {
                return "store";
            }
        }

        public void Execute(params object[] executeArgs)
        {
            if(CommandArgs.Length == 2)
            {
                GameWorld world = executeArgs[0] as GameWorld;
                string destVar = (string)CommandArgs[0];
                string srcVar = (string)CommandArgs[1];
                if (destVar.StartsWith("%"))//local var
                {
                    if (srcVar.StartsWith("%"))
                    {
                        context.ChangeValue(destVar.Substring(1, destVar.IndexOf(destVar.Last())), context.GetValue(srcVar.Substring(1, srcVar.IndexOf(srcVar.Last()))));
                    }
                    else if (srcVar.StartsWith("$"))
                    {
                        context.ChangeValue(destVar.Substring(1, destVar.IndexOf(destVar.Last())), world.GetValue(srcVar.Substring(1, srcVar.IndexOf(srcVar.Last()))));
                    }
                }
                else if (destVar.StartsWith("$"))//global var
                {
                    if (srcVar.StartsWith("%"))
                    {
                        world.ChangeValue(destVar.Substring(1, destVar.IndexOf(destVar.Last())), context.GetValue(srcVar.Substring(1, srcVar.IndexOf(srcVar.Last()))));
                    }
                    else if (srcVar.StartsWith("$"))
                    {
                        world.ChangeValue(destVar.Substring(1, destVar.IndexOf(destVar.Last())), world.GetValue(srcVar.Substring(1, srcVar.IndexOf(srcVar.Last()))));
                    }
                }
            }
        }

        public void PushArg(string cmdArg, int index)
        {
            throw new NotImplementedException();
        }
    }
}
