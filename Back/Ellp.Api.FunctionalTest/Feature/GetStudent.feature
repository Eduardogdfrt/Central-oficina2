Feature: GetStudent
  As a user
  I want to get the details of a student by email and password
  So that I can verify the student's information

  Scenario: Get student details by email and password
    Given I have the student email "edufreits002@gmail.com" and password "edu123"
    When I request the student details
    Then the response should be successful
    And the student details should be returned
