<div align="center">
  <h1 align="center">Working app that demonstrates the various flows in OpenId 🚀</a></h1>
  <p align="left">
    This repository uses an MVC app which then communicates with an API to demonstrate the various flows in OpenId. It uses Duende's Identity Server as the IDP. You'll also see a React app which uses the Backend for Frontend (BFF) pattern to communicate with the API along with a a pure Javascript client which again uses the BFF pattern to communicate with the API.
  </p>
<p align="left">
 
</p>
</div>

## Setup

1.  Clone the repository.
2.  Open the solution in Visual Studio and ensure that `Multiple startup projects` radio button is selected and that all the projects have the `Start` action.
3.  The user dB database must be created and seeded. To do this, open the Package Manager Console and ensure that the default project is set to `MakeBitByte.IDP`.
    Then run the following command - `update-database -Context UserDbContext`

This will create the database and seed it with the default users.
The default users are:

- `appa` with password `P@ssw0rd`
- `arjun` with password `P@ssw0rd`
- `vinita` with password `P@ssw0rd`

## Auth code with PKCE flow

For this, go to the `NoteController` in the `Notes.MvcApp` project and uncomment the `[Authorize]` attribute. This should be the only `Authorize` attribute that should be uncommented. This will then cause the `.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => { ... })` to be called.

Also, ensure that the default challege scheme in the `program.cs` file of the `Notes.MvcApp` is set to ` options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme` within the `builder.Services.AddAuthentication(options => { ... })` method. This will ensure that the `.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => { ... })` handler is used for the challenge.

## Authentication with Private Key JWT

For this, go to the `NoteController` in the `Notes.MvcApp` project and uncomment the `[Authorize(AuthenticationSchemes = "CodeFlowWithPrivateKeyJWTScheme")]` attribute. This should be the only `Authorize` attribute that should be uncommented. This will then cause the `.AddOpenIdConnect("CodeFlowWithPrivateKeyJWTScheme", options => { ... })` to be called.

Also, ensure that the default challege scheme in the `program.cs` file of the `Notes.MvcApp` is set to `options.DefaultChallengeScheme = "CodeFlowWithPrivateKeyJWTScheme"` within the `builder.Services.AddAuthentication(options => { ... })` method. This will ensure that this time the `.AddOpenIdConnect("CodeFlowWithPrivateKeyJWTScheme", options => { ... })` handler is used for the challenge.

## JAR or JWT secured authorization request with client secret

For this, go to the `NoteController` in the `Notes.MvcApp` project and uncomment the `[Authorize(AuthenticationSchemes = "CodeFlowWithJARScheme")]` attribute. This should be the only `Authorize` attribute that should be uncommented. This will then cause the `.AddOpenIdConnect("CodeFlowWithJARScheme", options => { ... })` to be called.

As before you'll want to make sure the corresponding default challenge scheme is set to `CodeFlowWithJARScheme` in the `program.cs` file of the `Notes.MvcApp` project.

## Token encrypted code flow

As before, you'll only want to uncomment `[Authorize(AuthenticationSchemes = "CodeFlowWithTokenEncryptionScheme")]` in the `NoteController` in the `Notes.MvcApp` project. This will then cause the `.AddOpenIdConnect("CodeFlowWithTokenEncryptionScheme", options => { ... })` to be called.

You'll also want to make sure the corresponding default challenge scheme is set to `CodeFlowWithTokenEncryptionScheme` in the `program.cs` file of the `Notes.MvcApp` project.

Also, make sure `builder.Services.AddTransient<ITokenCreationService, EncryptedTokenCreationService>()` is uncommented in the `HostingExtensions.cs` file of the `MakeBitByte.IDP` project.

## Demonstrate proof of possession (DPoP)

As before, you'll only want to uncomment `[Authorize(AuthenticationSchemes = "CodeFlowWithDPoPScheme")]` in the `NoteController` in the `Notes.MvcApp` project. This will then cause the `.AddOpenIdConnect("CodeFlowWithDPoPScheme", options => { ... })` to be called. The corresponding challenge scheme should be set to `CodeFlowWithDPoPScheme` in the `program.cs` file of the `Notes.MvcApp` project.

Make sure `builder.Services.ConfigureDPoPTokensForScheme(JwtBearerDefaults.AuthenticationScheme)` in the `program.cs` file of the `Notes.API` project is uncommented.

You'll also want to uncomment the following lines of code in the `NoteController` for all the actions. This will ensure that the DPoP token is validated. You'll find the `NoteController` in the `Notes.API` project.

```csharp
  var proofToken = Request.GetDPoPProofToken();
  if (proofToken == null) return BadRequest();
```

## Backend for Frontend (BFF) pattern

Navigate to the `ReactClientApp` folder and then `npm install` followed by `npm run dev`
You should now be able to login and see the notes being retuned from the API.

Added a [blog post](https://www.makebitbyte.com/blog/lets-make-our-spa-more-secure-by-setting-up-a-net-bff-with-duende) which explains how the BFF pattern works while securing a react front end app.
