Feature: GetProfessors
  As a user
  I want to get the details of a professor by ID and password
  So that I can verify the professor's information

  Scenario: Get professor details by ID and password
    Given I have the professor ID "23106801" and password "password"
    When I request the professor details
    Then the response should be successful
    And the professor details should be returned
