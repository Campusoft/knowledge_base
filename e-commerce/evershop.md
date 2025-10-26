# evershop


# API


API Request Data Validation

EverShop allows you to define validation schemas for API request data using a payloadSchema.json file in the API endpoint folder. This ensures that incoming data meets your requirements before processing.

When a request is made to the API endpoint, the request data is automatically validated against this schema. If validation fails, an appropriate error response is sent to the client with details about the validation issues.


EverShop uses Ajv JSON schema validator for request payload validation, providing robust and flexible validation capabilities.


https://evershop.io/docs/development/knowledge-base/api-routes#api-request-data-validation
