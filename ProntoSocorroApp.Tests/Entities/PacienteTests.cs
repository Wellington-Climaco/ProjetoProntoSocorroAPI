

// using Domain.Entities;
// using Domain.Enums;

// namespace ProntoSocorroApp.Tests.Entities
// {
//     [TestClass]

//     public class PacienteTests
//     {

//         [TestMethod]
//         [TestCategory("Domain")]

//         public void Quando_criado_novo_paciente_o_statusAtendimento_deve_ser_triagem()
//         {
//             var paciente = new Paciente("Wellington", "Clímaco", "12345678912", new DateTime(2001, 03, 20), EPreferencial.N);
//             Assert.AreEqual(EStatusAtendimento.Triagem,paciente.StatusAtendimento);
//         }

//         [TestMethod]
//         [TestCategory("Domain")]

//         public void Quando_criar_paciente_com_valores_validos_deve_retornar_true_para_isvalid()
//         {
//             var paciente = new Paciente("Wellington", "Clímaco", "12345678912", new DateTime(2001, 03, 20), EPreferencial.N);
//             Assert.IsTrue(paciente.IsValid);
//         }

//         [TestMethod]
//         [TestCategory("Domain")]

//         public void Quando_criar_paciente_com_valores_invalidos_deve_retornar_false_para_isvalid()
//         {
//             var paciente = new Paciente("", "Clímaco", "12345678912", new DateTime(2025, 03, 20), EPreferencial.N);
//             Assert.IsFalse(paciente.IsValid);
//         }

//         [TestMethod]
//         [TestCategory("Domain")]

//         public void Quando_criar_paciente_com_valores_invalidos_deve_gerar_notificacoes()
//         {
//             var paciente = new Paciente("", "Clímaco", "12345678912", new DateTime(2025, 03, 20), EPreferencial.N);
//             CollectionAssert.Contains(paciente.Notifications, "Nome não pode ser vazio");
//             CollectionAssert.Contains(paciente.Notifications, "Data nascimento Inválida");
//         }

//         [TestMethod]
//         [TestCategory("Domain")]

//         public void Quando_criar_paciente_com_status_preferencial_invalido_deve_retornar_notificacoes()
//         {
//             var paciente = new Paciente("Wellington", "Clímaco", "12345678912", new DateTime(2001, 03, 20), (EPreferencial)8);

//             Assert.IsFalse(paciente.IsValid);

//             CollectionAssert.Contains(paciente.Notifications,"situação preferencial inválida");
//         }

//         [TestMethod]
//         [TestCategory("Domain")]

//         public void Quando_atualizar_paciente_deve_atualizar_somente_campos_preenchidos()
//         {
//             var paciente = new Paciente("Wellington", "Clímaco", "12345678912", new DateTime(2001, 03, 20), (EPreferencial)8);
//             var novoSobrenome = "Clímaco";
//             var novaData = new DateTime(2001, 03, 12);

//             paciente.Update("", novoSobrenome, "", novaData, EPreferencial.S);

//             Assert.AreEqual(novoSobrenome,paciente.Sobrenome);
//             Assert.AreEqual(novaData, paciente.DataNascimento);
//             Assert.AreEqual("Wellington", paciente.Nome);
//             Assert.AreEqual("12345678912", paciente.Documento);
//         }

//         [TestMethod]
//         [TestCategory("Domain")]

//         public void Quando_paciente_for_encaminhado_para_area_inexistente_retornar_notificacao()
//         {
//             var paciente = new Paciente("Wellington", "Clímaco", "12345678912", new DateTime(2001, 03, 20), EPreferencial.N);

//             paciente.Encaminhar((EStatusAtendimento)8);

//             CollectionAssert.Contains(paciente.Notifications,"Encaminhamento inválido");
//         }

//         [TestMethod]
//         [TestCategory("Domain")]

//         public void Quando_paciente_for_encaminhado_corretamente_deve_atualizar_o_statusAtendimento_do_paciente()
//         {
//             var paciente = new Paciente("Wellington", "Clímaco", "12345678912", new DateTime(2001, 03, 20), EPreferencial.N);

//             paciente.Encaminhar(EStatusAtendimento.Clinico);

//             Assert.AreEqual(EStatusAtendimento.Clinico, paciente.StatusAtendimento);          
//         }





//     }
// }
