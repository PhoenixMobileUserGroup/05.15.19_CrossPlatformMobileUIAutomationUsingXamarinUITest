
Feature: Demo

Scenario: To Do - Add New
	:Given I am on the To Do list view
	When I select add new to do    
    Then I should be navigated to the new to do view
    When I enter new To Do as following
    |Name                       |Notes      |
    |Xamarin SpecFlow Demo      |BDD UI Test|
    And I choose to save the new To Do
    Then I should be navigated to the To Do list view
    And I should see Xamarin Demo in the list