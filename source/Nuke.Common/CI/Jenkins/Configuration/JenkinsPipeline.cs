﻿// Copyright 2021 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using Nuke.Common.CI.Jenkins.Configuration.Parameters;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.CI.Jenkins.Configuration
{
    /// <summary>
    /// Represents a declarative Jenkins pipeline, see <see href="https://www.jenkins.io/doc/book/pipeline/syntax/#declarative-pipeline">Declarative Pipeline</see>.
    /// </summary>
    public class JenkinsPipeline : ConfigurationEntity
    {
        private readonly IEnumerable<Parameter> _parameters;
        private readonly IEnumerable<Stage> _stages;
        private readonly Agent _agent;

        /// <summary>
        /// Initializes a new instance of <see cref="JenkinsPipeline"/> class.
        /// </summary>
        /// <param name="agent">Agent.</param>
        /// <param name="parameters">List of parameters.</param>
        /// <param name="stages">List of stages.</param>
        public JenkinsPipeline(Agent agent, IEnumerable<Parameter> parameters, IEnumerable<Stage> stages)
        {
            _agent = agent;
            _parameters = parameters;
            _stages = stages;
        }

        /// <inheritdoc />
        public override void Write(CustomFileWriter writer)
        {
            using (writer.WriteBlock("pipeline"))
            {
                _agent.Write(writer);

                if (_parameters?.Any() ?? false)
                {
                    using (writer.WriteBlock("parameters"))
                    {
                        _parameters.ForEach(x => x.Write(writer));
                    }
                }

                if (_stages?.Any() ?? false)
                {
                    using (writer.WriteBlock("stages"))
                    {
                        _stages.ForEach(x => x.Write(writer));
                    }
                }
            }
        }
    }
}
