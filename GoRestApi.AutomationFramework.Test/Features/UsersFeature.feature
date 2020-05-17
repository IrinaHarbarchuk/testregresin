Feature: UsersFeature
	In order to avoid errors with GoRest users api
	I want to run these tests

@regression
Scenario: 'Get all users' api call with valid request returns users collection 
When I get all users via GoRest api
Then I see that 200 status code was returned in response
And I see that users list was returned in response
