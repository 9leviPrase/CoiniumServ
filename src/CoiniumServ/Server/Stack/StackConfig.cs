﻿#region License
// 
//     CoiniumServ - Crypto Currency Mining Pool Server Software
//     Copyright (C) 2013 - 2014, CoiniumServ Project - http://www.coinium.org
//     http://www.coiniumserv.com - https://github.com/CoiniumServ/CoiniumServ
// 
//     This software is dual-licensed: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//    
//     For the terms of this license, see licenses/gpl_v3.txt.
// 
//     Alternatively, you can license this software under a commercial
//     license or white-label it as set out in licenses/commercial.txt.
// 
#endregion
using System;
using System.Collections.Generic;
using JsonConfig;
using Serilog;

namespace CoiniumServ.Server.Stack
{
    public class StackConfig : IStackConfig
    {
        public bool Valid { get; private set; }
        public string Name { get; private set; }
        public IList<IStackNode> Nodes { get; private set; }

        public StackConfig(dynamic config)
        {
            try
            {
                // load the config data.
                Name = string.IsNullOrEmpty(config.name) ? "CoiniumServ.com" : config.name;

                Nodes = new List<IStackNode>();

                if (config.nodes is NullExceptionPreventer)
                {
                    Valid = true;
                    return;
                }

                foreach (var entry in config.nodes)
                {
                    Nodes.Add(new StackNode(entry));
                }

                Valid = true;
            }
            catch (Exception e)
            {
                Valid = false;
                Log.Logger.ForContext<StackConfig>().Error(e, "Error loading stack configuration");
            }
        }
    }
}
