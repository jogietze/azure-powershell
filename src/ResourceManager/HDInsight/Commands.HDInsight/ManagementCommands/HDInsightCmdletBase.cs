﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.HDInsight.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.HDInsight.Commands
{
    public class HDInsightCmdletBase : AzurePSCmdlet
    {
        private AzureHdInsightManagementClient _hdInsightManagementClient;
        private AzureHdInsightJobManagementClient _hdInsightJobClient;

        public AzureHdInsightManagementClient HDInsightManagementClient
        {
            get {
                return _hdInsightManagementClient ??
                       (_hdInsightManagementClient = new AzureHdInsightManagementClient(Profile.Context));
            }
            set { _hdInsightManagementClient = value; }
        }

        public AzureHdInsightJobManagementClient HDInsightJobClient
        {
            get
            {
                return _hdInsightJobClient ??
                    (_hdInsightJobClient = new AzureHdInsightJobManagementClient(Profile.Context));
            }
            set { _hdInsightJobClient = value; }
        }
    }
}
