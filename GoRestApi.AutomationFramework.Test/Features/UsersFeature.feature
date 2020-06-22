Feature: UsersFeature
	In order to avoid errors with reqres.in users api
	I want to run these tests

@regression
Scenario: 'Get all users' from 2 page api call with valid request returns users collection 
When I get all users from 2 page(-s) via reqres.in api
Then I see that 200 status code was returned in response
And I see that users list from 2 page was returned in response

@regression @usersAPI @smoke @api
Scenario Outline: I can create new user on reqres.in api
Given I have created user data as follow
| name      | job      |
| test_user | test_job |
When I create new user with data prepared via reqres.in api
Then I see that 201 status code was returned in response
And I get user with id of created user via reqres.in api
And I see that created user matches created one










