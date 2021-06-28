﻿#region license
// Transformalize
// Configurable Extract, Transform, and Load
// Copyright 2013-2019 Dale Newman
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using Autofac;
using System.IO;
using Transformalize.Configuration;

namespace Transformalize.Providers.Json.Autofac {
   public class JsonProviderModule : Module {
      private readonly StreamWriter _streamWriter;
      public bool UseAsyncMethods { get; set; }

      public JsonProviderModule(StreamWriter streamWriter = null) {
         _streamWriter = streamWriter;
      }

      protected override void Load(ContainerBuilder builder) {

         if (!builder.Properties.ContainsKey("Process")) {
            return;
         }

         var process = (Process)builder.Properties["Process"];

         var b = new JsonProviderBuilder(process, builder, _streamWriter) { UseAsyncMethods = UseAsyncMethods };
         b.Build();
      }
   }
}