using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas_no_Caixa.DAL;

namespace Vendas_no_Caixa.Modelo
{
    public class Controle
    {
        public bool tem;
        public string mensagem = "";
        public bool acessar(String login, String senha)
        {
            LoginDaoComandos LoginDao = new LoginDaoComandos();
            LoginDao.verificarLogin(login, senha);
            tem = LoginDao.verificarLogin(login, senha);
            if(!LoginDao.mensagem.Equals(""))
            {
                this.mensagem = LoginDao.mensagem; 
            }
            return tem;
        }

        public string cadastrar(String email, String senha, String confSenha)
        {
            return mensagem;
        }
        
         
    
    }
}
