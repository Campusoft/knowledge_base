# Conductor  

Conductor is a workflow orchestration engine that runs in the cloud.

Conductor is an open-source, Apache 2.0 licensed workflow orchestration framework. You can use Conductor to easily build highly reliable distributed applications using the language of your choice.

# Concepts


Definitions (aka Metadata or Blueprints)
Tasks

-    System tasks - executed by Conductor server.
-    Worker tasks - executed by your own workers. The worker tasks can be implemented in any language. These tasks talk to Conductor server via REST/gRPC to poll for tasks and update its status after execution.

Workflow

https://netflix.github.io/conductor/gettingstarted/basicconcepts/