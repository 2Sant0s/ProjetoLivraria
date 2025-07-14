<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoAutores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoAutores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="50%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="" ColCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    <dx:LayoutItem Caption="Nome">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txbCadastroNomeAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o nome do Autor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
                <Items>
                    <dx:LayoutItem Caption="Sobrenome">
                        <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="txbCadastroSobrenomeAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o sobrenome do Autor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                        <dx:LayoutItem Caption="E-mail" ColSpan="2">
                            <layoutitemnestedcontrolcollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxTextBox ID="txbCadastroEmailAutor" runat="server" Width="100%">
                                        <validationsettings validationgroup="MyGroup" validateonleave="true" display="Dynamic">
                                            <requiredfield isrequired="true" errortext="Digite o email do Autor" />
                                            <regularexpression errortext="Email inválido" validationexpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                                        </validationsettings>
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </layoutitemnestedcontrolcollection>
                        </dx:LayoutItem>

                        <dx:LayoutItem Caption="" ColSpan="2">
                            <layoutitemnestedcontrolcollection>
                                <dx:LayoutItemNestedControlContainer runat="server">
                                    <dx:ASPxButton runat="server" Text="Salvar" AutoPostBack="false" Width="100%" OnClick="BtnNovoAutor_Click" />
                                </dx:LayoutItemNestedControlContainer>
                            </layoutitemnestedcontrolcollection>
                        </dx:LayoutItem>
                    </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
