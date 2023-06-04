Unit-testing

This is a Bankapp originally made by some of my classmates, who kindly has let me use it for learning Unittests, using MsTest.

I chose to test: 

LogIn in User: Login_With_Correct_User_Name(), Login_With_Correct_Password() and Login_With_Correct_User_And_Password(), all these test that a User logs in with maching name and password.

LoanLimit in LoanAccount: LoanLimit_Should_Not_Be_Able_To_Loan_Over_Limit() tests that users are not able to loan more than 5 times their amount on an account.

ProcessTransaction in Transaction: ProcessTransaction_Balance_Should_Be_Correct() tests that the correct amount is transferred to the chosen account.
ProcessTransaction_Balance_Should_Not_Be_Negative() tests that it is not possible to transfer more money than exists on the account.

Thanks to the original creators of this app: 
* [Alfred Larsson](https://github.com/Fredihi)
* [Kenny Lindblom](https://github.com/KennyLindblom)
* [Martin Qvarnström](https://github.com/qvarnstr0m)
* [Nour Uqla](https://github.com/NourUq02)
* [Zanefina Qmega](https://github.com/Zanefina)
 
 Team Grape Bank App (https://github.com/qvarnstr0m/TeamGrapeBankApp)
