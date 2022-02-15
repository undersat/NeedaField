// Copyright (c) 2021 UNDERSAT IT S.L. <eluis@undersat.com>
// Licensed to UNDERSAT IT S.L. under one or more agreements.
// UNDERSAT IT S.L. licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;


public class NeedaField
{
    public string dina_property { get; private set; } = "";
    private string dina_class { get; set; } = "";
    private string json_definition = "";

    public class Dinafield
    {
        public string val { get; set; } = "";
        public string type { get; set; } = "text";
    }

    public Dictionary<string, Dinafield> fields { get; set; }

    public NeedaField(string json_current_values = "", string my_json_definition = "")
    {
        if (!caller_info())
        {
            throw new Exception("No info from caller entity");
        }

        if (string.IsNullOrEmpty(my_json_definition))
        {
            string param_name = dina_property + "_" + dina_class;
            json_definition = System.Configuration.ConfigurationManager.AppSettings[param_name];
            if (string.IsNullOrEmpty(json_definition))
            {
                throw new Exception("No parameter in appsettings named " + param_name);
            }
        }
        else
        {
            json_definition = my_json_definition;
        }


        fields = new Dictionary<string, Dinafield>(StringComparer.OrdinalIgnoreCase);
        try
        {
            JsonConvert.PopulateObject(json_definition, fields);
        }
        catch (Exception ex)
        {
            throw new Exception("Invalid or empty json definition [" + json_definition + "]", ex.InnerException);
        }

        if (!string.IsNullOrEmpty(json_current_values))
        {
            // apply current values and/or add new ones typed as text

            try
            {
                foreach (var curval in JsonConvert.DeserializeObject<Dictionary<string, string>>(json_current_values))
                {
                    if (!fields.ContainsKey(curval.Key))
                    {
                        fields.Add(curval.Key, new Dinafield() { val = curval.Value });
                    }
                    else
                    {
                        fields[curval.Key].val = curval.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public override string ToString()
    {
        var dic = new Dictionary<string, string>();

        foreach (var field in fields)
        {
            dic.Add(field.Key, field.Value.val);
        }

        return JsonConvert.SerializeObject(dic);
    }

    public string where(object key, object value)
    {
        string template = dina_property + " like '%\"{0}\":\"{1}\"%'";
        return string.Format(template, key, value);
    }

    public string getfield(string key)
    {
        Dinafield _df = null;
        if (fields.TryGetValue(key, out _df))
            return _df.val;
        return "";
    }

    public int setfield(string key, string value)
    {
        if (!fields.ContainsKey(key))
        {
            fields.Add(key, new Dinafield() { val = value });
            return 1;
        }
        else
        {
            fields[key].val = value;
            return 0;
        }
    }

    private bool caller_info() // get class and property name who is using etcetera
    {
        foreach (var caller in (new StackTrace()).GetFrames())
        {
            var method = caller.GetMethod();
            if (method.Name.StartsWith("get_") || method.Name.StartsWith("set_"))
            {
                dina_property = method.Name.Substring(4);
                dina_class = method.DeclaringType.Name;
                return true;
            }
        }
        return false;
    }
}

