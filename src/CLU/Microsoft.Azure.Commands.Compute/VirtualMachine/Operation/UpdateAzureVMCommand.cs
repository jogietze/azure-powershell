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

using AutoMapper;
using Microsoft.Azure.Commands.Compute.Common;
using Microsoft.Azure.Commands.Compute.Models;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using System.Collections;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Compute
{
    [Cmdlet(VerbsData.Update, ProfileNouns.VirtualMachine, DefaultParameterSetName = ResourceGroupNameParameterSet)]
    [OutputType(typeof(PSComputeLongRunningOperation))]
    [CliCommandAlias("vm;update")]
    public class UpdateAzureVMCommand : VirtualMachineActionBaseCmdlet
    {
        [Alias("VMProfile")]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public PSVirtualMachine VM { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public Hashtable[] Tags { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            ExecuteClientAction(() =>
            {
                var parameters = new VirtualMachine
                {
                    DiagnosticsProfile = this.VM.DiagnosticsProfile,
                    HardwareProfile = this.VM.HardwareProfile,
                    StorageProfile = this.VM.StorageProfile,
                    NetworkProfile = this.VM.NetworkProfile,
                    OsProfile = this.VM.OSProfile,
                    Plan = this.VM.Plan,
                    AvailabilitySet = this.VM.AvailabilitySetReference,
                    Location = this.VM.Location,
                    Tags = this.Tags != null ? this.Tags.ToDictionary() : this.VM.Tags
                };

                var op = this.VirtualMachineClient.CreateOrUpdate(this.ResourceGroupName, this.VM.Name, parameters);
                // TODO: CLU
                var result = op;
                //var result = Mapper.Map<PSComputeLongRunningOperation>(op);
                WriteObject(result);
            });
        }
    }
}