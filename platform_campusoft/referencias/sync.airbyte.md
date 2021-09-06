# airbyte

Technical Stack
https://docs.airbyte.io/understanding-airbyte/tech-stack

High-level View
A high level view of Airbyte's components.
https://docs.airbyte.io/understanding-airbyte/high-level-view


## ConnectorSpecification

- The UI reads the JsonSchema in this field in order to render the input fields for a user to fill in.
- This JsonSchema is also used to validate that the provided inputs are valid. e.g. If port is one of the fields and the JsonSchema in the connectorSpecification specifies that this filed should be a number, if a user inputs "airbyte", they will receive an error. Airbyte adheres to JsonSchema validation rules.

## AirbyteCatalog

An AirbyteCatalog describes the structure of data in a data source. It has a single field called streams that contains a list of AirbyteStreams. Each of these contain a name and json_schema field. The json_schema field accepts any valid JsonSchema and describes the structure of a stream. This data model is intentionally flexible. 



