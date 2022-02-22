
# Shopping App
A WPF desktop application in C#, using XAML for UI design, which allows customer users and admin users to log in, was created by Ximena Carrillo and Jair Fonseca.

## Usage
### How Play
#### To Consider:
- Before running the program, please execute the script "db.sql" located in the path "/Scripts"
- Configure App.config according to your connection string.
- Credentials:
 - User Admin: Username: Admin Password: otheradmin
 - User Client: Username: JaredC Password: JaredC
#### To Start
- The user must type their username and password. Then press the "Login" button.
- The system shows the principal window named Purchase window.
#### To Logout
- Press button "Logout" on right-top corner.
#### Purchase Order
##### Products
- Select a product on the grid.
- You can see the product selected at the bottom of the grid.
- Enter the quantity desired to buy on the textbox. Note that next to the text box there is a number corresponding to the stock available for the product.
- Press Add to Cart.
- Note: The button Add to Cart will be enabled only if the number on the text box is greater or equal to the stock.
##### Cart
- When you press the button "Add to Cart" on the Products section, a new item will be added to the grid on "Cart" section.
- Once you selected an item of the cart you will be able to:
  -  Add Unit: Add one unit to the product
  -  Remove Unit: Remove one unit to the product
  -  Remove from Cart: remove an item from the cart.
  -  Empty cart: Remove all items from the cart.
##### Checkout
- In this section, you will be able to see the total of your order.
- Each time you add a product to the cart, the checkout will be updated.
- To finish the order, please press the Place Order button
- You will be notified of the success of the purchase and the number of the order.

## Adittional Features
##### Order history
- If you are a client you can see your orders by pressing the button "My Orders" located on the top banner, next to your name.
- If you are an admin you can see all orders purchased and the total of sales for the store, by pressing the button "All Orders" located on the top banner next to your name.
##### Filter Products by Name
- On the purchase window, section Products: You can filter the list of products using the textbox over the grid.
- You can also clear the filter (means show all products again) using the "Clear" button.

If you want, you can see our code on: https://github.com/ximenacarrillo/ShoppingApp_XC_JF
