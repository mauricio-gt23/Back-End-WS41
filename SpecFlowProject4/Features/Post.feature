Feature: Post
	Create a post

@mytag


Scenario: Add new Post with below details
	When post required attributes provided
	| Id | Title | Address   | Province | District | Department | Price | RoomQuantity | BathroomQuantity | PostDate   | LandlordId |
	| 1  | Titulo | Direccion | Lima     | Lima     | Lima       | 1990  | 2            | 2                | 2020/12/31| 1          |