﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capture.Workflow.Core.Classes;
using Capture.Workflow.Core.Classes.Attributes;
using Capture.Workflow.Core.Interface;
using Jint;


namespace Capture.Workflow.Plugins.Commands
{
    [Description("")]
    [PluginType(PluginType.Command)]
    [DisplayName("MathAction")]
    public class MathAction : IWorkflowCommand
    {
        public string Name { get; set; }
        public WorkFlowCommand CreateCommand()
        {
            var command = new WorkFlowCommand();
            command.Properties.Add(new CustomProperty()
            {
                Name = "Variable",
                PropertyType = CustomPropertyType.Variable,
            });
            //command.Properties.Add(new CustomProperty()
            //{
            //    Name = "Action",
            //    PropertyType = CustomPropertyType.ValueList,
            //    ValueList = new List<string>() { "Increment", "Set" },
            //    Value = "Increment"
            //});
            command.Properties.Add(new CustomProperty()
            {
                Name = "Formula",
                PropertyType = CustomPropertyType.String,
            });
            return command;
        }

        public bool Execute(WorkFlowCommand command, Context context)
        {
            var var = new Engine()
                    //.SetValue("x", 3) // define a new variable
                    //.Execute("x * x") // execute a statement
                    //.GetCompletionValue() // get the latest statement completion value
                    //.ToObject() // converts the value to .NET
                ;


            foreach (var variable in context.WorkFlow.Variables.Items)
            {
                //e.Parameters[variable.Name] = new Exception(variable.Value);
                var.SetValue(variable.Name, variable.GetAsObject());
            }
            
            context.WorkFlow.Variables[command.Properties["Variable"].Value].Value = var.Execute(command.Properties["Formula"].Value).GetCompletionValue().ToString(); 

            return true;
        }
    }
}