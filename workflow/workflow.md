# workflow   

# workflow engine

How workflow engines work

Defining your workflow in some declarative format. Some engines use a JSON like structure, others use REST (which comes down to JSON as well), some expose a library which allows you to code the order in which steps should be executed and the API will take care of tracking progress and unhappy flows, and the list goes on. The workflow definition contains all steps/tasks that need to be executed, together with their order (sequential, parallel or both) and they can even hold placeholders for input parameters for each step/task. 


After the workflow is defined, the next step is to run it. How/when to start a run is entirely up to the design of your system. It can be triggered manually, time based or automatically whenever something happens (for example if a user uploads a file, start the workflow). 

After you’ve started a workflow run, it will go start at the first step in the definition and execute it. Once it finishes, the next step will start to run automatically. Some workflow engines even provide an interactive UI in which you can track the overall progress of the run. 


AWS Step functions: A cloud-based workflow engine managed by AWS. Workflows are defined using a JSON like syntax or using the visual editor, and the service offers a visual representation of your definition. Executions, which are also visualized, can be triggered manually, or via events.

Azure Logic Apps: A cloud-based workflow engine managed by Azure. 
  
Google Cloud WorkFlows: A cloud-based workflow engine managed by Google Cloud. Workflows can be defined in either YAML or JSON format. Furthermore, the orchestrator uses HTTP to interact with the tasks.
 
Netflix conductor: Netflix Conductor is an open-source workflow engine created by Netflix. It uses REST to define, update and start workflows (make rest calls to the engine with your workflow defined as JSON). It provides a GUI to track progress of an execution and it provides libraries for multiple programming languages which can be used to implement the integration layer on your tasks.

Apache Airflow: Apache Airflow is an open-source workflow engine created by Airbnb. It allows users to create their workflow definitions using Python code. The workflow can be executed based on a specified time interval (Cron based) or by an external (or manual) trigger (via the cli or web UI for example). Airflow also provides a UI in which you can monitor the progress of an execution

data\Airflow.md

## Referencias


Introduction to workflow engines
Orchestration as a service

https://www2.deloitte.com/nl/nl/blog/deloitte-digital/2021/introduction-to-workflow-engines.html

 