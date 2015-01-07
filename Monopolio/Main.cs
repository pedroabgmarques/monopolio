using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace Monopolio
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Monopolio : Microsoft.Xna.Framework.Game
    {

        #region Estado
        /// <summary>
        /// Permite aceder a informações acerca da capacidade gráfica
        /// </summary>
        GraphicsDeviceManager graphics;
        /// <summary>
        /// Estado atual do teclado
        /// </summary>
        KeyboardState estadoTeclado;
        /// <summary>
        /// Spritebatch usado para desenhar o jogo
        /// </summary>
        SpriteBatch spriteBatch;
        /// <summary>
        /// Lista de camadas do efeito parallax
        /// </summary>
        List<ParallaxLayer> layers;
        /// <summary>
        /// Lista de jogadores
        /// </summary>
        List<Jogador> listaJogadores;
        /// <summary>
        /// Lista de componentes de UI
        /// </summary>
        List<UI> listaComponentesUI;
        /// <summary>
        /// Fonte Arial, tamanho 12
        /// </summary>
        SpriteFont arial12;
        /// <summary>
        /// Contém o estado e eventos do rato
        /// </summary>
        Rato rato;
        /// <summary>
        /// Tabuleiro de jogo
        /// </summary>
        Tabuleiro tabuleiro;
        /// <summary>
        /// Camara 2D com movimento, rotação e zoom
        /// </summary>
        Camera camera;
        /// <summary>
        /// ViewMatriz utilizada para desenhar o jogo
        /// </summary>
        Matrix ViewMatrix;
        /// <summary>
        /// Textura utilizada para sobreposição das casa de jogo
        /// </summary>
        Texture2D debugRectangle;
        /// <summary>
        /// Guarda a janela modal ativa, para impedir o clique noutras janelas
        /// </summary>
        UI UIModalAtiva;
        /// <summary>
        /// Utilizado e reclicado para criar opções para janelas modais
        /// </summary>
        Opcao opcao;
        /// <summary>
        /// Utilizado e reutilizado para gerar UI's centrais
        /// </summary>
        UI_Centrado UI_Centrado;
        /// <summary>
        /// Utilizado e reutilizado para gerar UI's de lançamento
        /// </summary>
        UI_Lancamento UI_Lancamento;
        /// <summary>
        /// Utilizado e reutilizado para gerar textos
        /// </summary>
        StringBuilder texto;
        /// <summary>
        /// Utilizado e reutilizado para gerar listas de opcoes
        /// </summary>
        List<Opcao> listaOpcoes;
        /// <summary>
        /// Utilizado e reutilizado para verificar se o rato está em cima de algo
        /// </summary>
        Rectangle rectanguloRato;
        /// <summary>
        /// Permite gerar números aleatórios
        /// </summary>
        Random random;
        /// <summary>
        /// Guarda a casa que está atualmente ativa
        /// </summary>
        Casa casaAtual;
        /// <summary>
        /// Gestor de animações da camera
        /// </summary>
        CameraAnimationManager cameraAnimationManager;
        /// <summary>
        /// Gestor de animações dos tokens dos jogadores
        /// </summary>
        TokenAnimationManager tokenAnimationManager;
        /// <summary>
        /// Vector2 reutilizável
        /// </summary>
        Vector2 posicao;

        /// <summary>
        /// TESTE - guarda o numero que saiu nos dados, que indica quantas casas vamos mover
        /// </summary>
        int casasAMover;
        /// <summary>
        /// Indice do jogador atual
        /// </summary>
        int indiceJogadorAtual;
        /// <summary>
        /// Jogador atual
        /// </summary>
        Jogador jogador;
        /// <summary>
        /// Splash "Player 1" - indica a vez de jogar
        /// </summary>
        PlayerAnimation playerSplashAnimation;
        /// <summary>
        /// Lista de animações de receber ou pagar dinheiro
        /// </summary>
        List<MoneyAnimation> listaMoneyAnimation;
        List<MoneyAnimation> deadMoneyAnimations;

        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        public Monopolio()
        {
            //Opções de gráficos
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true; //Anti-aliasing
            graphics.GraphicsProfile = GraphicsProfile.HiDef; //Gráficos potentes
            graphics.IsFullScreen = true; //Fullscreen
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 680;

            Content.RootDirectory = "Content";

            //Construir um gerador de numeros aleatorios
            random = new Random();
            
            //Construir um rato
            rato = new Rato();

            //Construir um tabuleiro
            tabuleiro = new Tabuleiro();

            //Inicializar a lista de jogadores;
            listaJogadores = new List<Jogador>();

            //Inicializar a lista de componentes de UI
            listaComponentesUI = new List<UI>();

            //Inicializar a lista de opções utilizada para gerar certos componentes de UI
            listaOpcoes = new List<Opcao>();

            //Inicilizar o StringBuilder utilizado para gerar textos
            texto = new StringBuilder();

            //Inicializar a lista de animações de receber ou pagar dinheiro
            listaMoneyAnimation = new List<MoneyAnimation>();
            deadMoneyAnimations = new List<MoneyAnimation>();

            //Inicializar o rectangulo do rato
            rectanguloRato = new Rectangle();
            rectanguloRato.Width = 5;
            rectanguloRato.Height = 5;

            //Setar o estado inicial do jogo
            GameState.Estado = Estado.Inicial;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arial12 = Content.Load<SpriteFont>("fontes/arial_12");
            
            //Carregar as texturas relacionadas com o rato
            rato.LoadContent(Content);
            //Subscrever os eventos do rato
            rato.clique += new Rato.cliqueHandler(processarCliquesRato);
            rato.desclique += new Rato.DescliqueHandler(processarDescliquesRato);

            //Load do sprite do tabuleiro
            tabuleiro.LoadContent(Content, GraphicsDevice);

            //Load das cartas de comunidade
            loadCartasComunidade();

            //load das cartas da sorte
            loadCartasSorte();

            //Construir uma camera
            camera = new Camera(GraphicsDevice, tabuleiro);

            //Inicializar o gestor de animações da camera
            cameraAnimationManager = new CameraAnimationManager();

            //Inicializar o gestor de animações dos tokens dos jogadores
            tokenAnimationManager = new TokenAnimationManager();

            debugRectangle = new Texture2D(GraphicsDevice, 1, 1);          

            //Criar uma lista de camadas para o efeito parallax
            gerarCamadasParallax();

            //Zoom Out Inicial
            cameraAnimationManager.newAnimation(Zoom.longe);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        #region Game Loop

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            //atualizar o estado do teclado
            estadoTeclado = Keyboard.GetState();

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || estadoTeclado.IsKeyDown(Keys.Escape))
                this.Exit();

            //Atualizar a posicao do rato
            rato.Update();

            //atualizar o rectangulo do rato
            atualizarRectanguloRato();

            //Atualizar as camadas de parallax
            atualizarCamadasParallax();

            //atualizar a camera
            camera.Update(estadoTeclado, GraphicsDevice, tabuleiro, cameraAnimationManager);

            //atualizar o gestor de animações da camera
            cameraAnimationManager.Update(camera);

            //atualizar o gestor de animações de tokens dos jogadores
            tokenAnimationManager.Update(tabuleiro);

            //atualizar o splash do jogador que tem a vez de jogar
            atualizarPlayerAnimationSplash();

            //atualizar animações de dinheiro
            atualizarMoneyAnimations();

            //Verificar se o rato está sobre um botao
            verificarRatoSobreBotao();

            //Limpar lista de componentes de UI
            eliminarUIsDesativas();

            //Correr a lógica do jogo
            gameLogic();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ViewMatrix = camera.getTransformation(Vector2.One);

            #region Parallax
            //desenhar o background / parallax
            foreach (ParallaxLayer layer in layers)
                layer.Draw(spriteBatch);

            desenharTabuleiro();

            desenharRectangulos();

            desenharDonosPropriedades();
            
            desenharTokensJogadores();

            #endregion

            #region No Parallax

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            desenharPlayerAnimationSplash();

            //desenhar seta do rato
            rato.Draw(spriteBatch);

            //desenhar coordenadas do rato [DEBUG]
            //desenharCoordenadasRato(rato);

            //desenhar coordenadas da camera [DEBUG]
            //desenharCoordenadasCamera(camera);

            //desenharRotacaoCamera [DEBUG]
            //desenharRotacaoCamera(camera);

            //desenhar UI
            desenharUI();

            desenharMoneyAnimations();
            
            spriteBatch.End();

            base.Draw(gameTime);
            #endregion
        }

        #endregion

        #region Helpers

        private void atualizarMoneyAnimations()
        {
            foreach (MoneyAnimation anim in listaMoneyAnimation)
            {
                anim.Update();
                if (!anim.Viva) deadMoneyAnimations.Add(anim);
            }
            foreach (MoneyAnimation anim in deadMoneyAnimations)
            {
                listaMoneyAnimation.Remove(anim);
            }
            deadMoneyAnimations.Clear(); 
        }

        /// <summary>
        /// Atualiza o splash que indica o jogador que tem a vez de jogar
        /// </summary>
        private void atualizarPlayerAnimationSplash()
        {
            if (playerSplashAnimation != null)
            {
                playerSplashAnimation.Update();
                if (!playerSplashAnimation.Viva) playerSplashAnimation = null;
            }
        }

        private void desenharMoneyAnimations()
        {
            foreach (MoneyAnimation anim in listaMoneyAnimation)
            {
                anim.Draw(arial12, spriteBatch);
            }
        }

        /// <summary>
        /// Desenha o splash que indica o jogador que tem a vez de jogar
        /// </summary>
        private void desenharPlayerAnimationSplash()
        {
            if (playerSplashAnimation != null)
            {
                playerSplashAnimation.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Objeto reutilizado para criar cartas de sorte
        /// </summary>
        CommunityAndChance cartaSorte;
        /// <summary>
        /// Gera uma fila de cartas de sorte
        /// </summary>
        private void loadCartasSorte()
        {
            
            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Advance to Go (Collect $200)", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, 0), x =>
                {
                    proximoJogador();
                });
                
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Advance to Trafalgar Square.", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, 24));
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Advance to the nearest utility.", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, tabuleiro.nearestUtility(jogador.CasaAtual, false)));
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Advance to the nearest railroad.", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, tabuleiro.nearestUtility(jogador.CasaAtual, true)));
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Advance to Pall Mall.", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, 11));
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Bank pays you dividend of $50", (s) =>
            {
                jogador.receber(50);
                proximoJogador();
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Get out of jail free: this card may be kept until needed, or sold", (s) =>
            {
                jogador.GetOutOfJail++;
                proximoJogador();
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Go back 3 spaces.", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, jogador.CasaAtual - 3));
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Mau, "Go directly to Jail.", (s) =>
            {
                goToPrison();
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Mau, "General repairs: for each house pay $25; for each hotel $100.", (s) =>
            {
                int conta = 25 * jogador.nCasas() + 100 * jogador.nHoteis();
                jogador.pagar(conta);
                proximoJogador();
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Mau, "Pay poor tax of $15", (s) =>
            {
                jogador.pagar(15);
                proximoJogador();
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Take a trip to Kings Cross Station.", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, 5));
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Take a trip to Mayfair.", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, 39));
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Mau, "You have been elected chairman of the board: pay each player $50", (s) =>
            {
                int contador = 0;
                foreach (Jogador jogador in listaJogadores)
                {
                    if (jogador != this.jogador)
                    {
                        jogador.receber(50);
                        contador++;
                    }
                }
                this.jogador.pagar(50 * contador);
                proximoJogador();
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "Your building loan matures: collect $150", (s) =>
            {
                jogador.receber(150);
                proximoJogador();
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

            cartaSorte = new CommunityAndChance(TipoOpcao.Bom, "You have won a crossword competition: collect $100 ", (s) =>
            {
                jogador.receber(100);
                proximoJogador();
            });
            tabuleiro.ListaChance.Enqueue(cartaSorte);

        }

        /// <summary>
        /// Objeto reutilizado para criar cartas de comunidade
        /// </summary>
        CommunityAndChance cartaComunidade;
        /// <summary>
        /// Gera uma fila de cartas de comunidade
        /// </summary>
        private void loadCartasComunidade()
        {
            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "Advance to Go (Collect $200)", (s) =>
            {
                moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, 0), x =>
                {
                    proximoJogador();
                });
                
                
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "Bank error in your favor: collect $75", (s) =>
            {
                jogador.receber(75);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Mau, "Doctor's fees: Pay $50", (s) =>
            {
                jogador.pagar(50);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "Get out of jail free: this card may be kept until needed, or sold", (s) =>
            {
                jogador.GetOutOfJail++;
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Mau, "Go to jail: go directly to jail: Do not pass Go, do not collect $200", (s) =>
            {
                goToPrison();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "It is your birthday! Collect $10 from each player", (s) =>
            {
                int contador = 0;
                foreach (Jogador jogador in listaJogadores)
                {
                    if (jogador != this.jogador)
                    {
                        jogador.pagar(10);
                        contador++;
                    }
                }
                this.jogador.receber(10 * contador);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "Grand Opera Night: collect $50 from every player for opening night seats", (s) =>
            {
                int contador = 0;
                foreach (Jogador jogador in listaJogadores)
                {
                    if (jogador != this.jogador)
                    {
                        jogador.pagar(50);
                        contador++;
                    }
                }
                this.jogador.receber(50 * contador);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "Income Tax refund: collect $20", (s) =>
            {
                jogador.receber(20);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "Life Insurance Matures: collect $100", (s) =>
            {
                jogador.receber(100);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Mau, "Pay Hospital Fees of $100", (s) =>
            {
                jogador.pagar(100);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Mau, "Pay School Fees of $50", (s) =>
            {
                jogador.pagar(50);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "Receive $25 Consultancy Fee", (s) =>
            {
                jogador.receber(25);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Mau, "Street repairs: $40 per house, $115 per hotel.", (s) =>
            {
                int conta = 40 * jogador.nCasas() + 115 * jogador.nHoteis();
                jogador.pagar(conta);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "You have won second prize in a beauty contest: collect $10", (s) =>
            {
                jogador.receber(10);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "You inherit $100", (s) =>
            {
                jogador.receber(100);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "From sale of stock you get $50", (s) =>
            {
                jogador.receber(50);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);

            cartaComunidade = new CommunityAndChance(TipoOpcao.Bom, "Holiday Fund matures: Receive $100", (s) =>
            {
                jogador.receber(100);
                proximoJogador();
            });
            tabuleiro.ListaCommunity.Enqueue(cartaComunidade);
        }

        private void desenharDonosPropriedades()
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                                BlendState.AlphaBlend,
                                null,
                                null,
                                null,
                                null,
                                ViewMatrix);
            foreach (Casa casa in tabuleiro.ListaCasas())
            {
                if (casa is Propriedade)
                {
                    Propriedade propriedade = (Propriedade)casa;
                    if(propriedade.Dono != null)
                        propriedade.Draw(spriteBatch, arial12, tabuleiro.ListaCasas().FindIndex(x => x == propriedade));
                }
            }
            spriteBatch.End();
        }

        /// <summary>
        /// Envia um jogador para a prisão
        /// </summary>
        private void goToPrison()
        {
            moverJogadorECameraNCasas(jogador.CasaAtual, tabuleiro.nCasasDiferenca(jogador.CasaAtual, 10), x =>
            {
                jogador.Jailed = true;
                proximoJogador();
            });
        }

        /// <summary>
        /// Desenha os tokens dos jogadores
        /// </summary>
        private void desenharTokensJogadores()
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                                BlendState.AlphaBlend,
                                null,
                                null,
                                null,
                                null,
                                ViewMatrix);
            foreach (Jogador jogador in listaJogadores)
            {
                jogador.Draw(spriteBatch, graphics.GraphicsDevice, this.jogador);
            }
            spriteBatch.End();
        }

        /// <summary>
        /// Move o jogador e a camera camara um determinado numero de casas
        /// </summary>
        /// <param name="indiceCasaAtual">Indice da casa que esta atualmente ativa</param>
        /// <param name="casasAMover">Nº de casas que vamos mover</param>
        private void moverJogadorECameraNCasas(int indiceCasaOriginal, int casasAMover, Action<string> accao = null)
        {
            if (camera.getZoom() != Zoom.medio) cameraAnimationManager.newAnimation(Zoom.medio, accao);
            jogador.CasaAtual = tabuleiro.IndiceCasaAFrente(jogador.CasaAtual, casasAMover);
            atualizarCasaAtual(jogador.CasaAtual);
            cameraAnimationManager.newAnimation(posicao, tabuleiro.verificarRotacaoEPartida(camera, indiceCasaOriginal, casasAMover, jogador), true);

            //Animar o bonequinho do jogador
            Vector2 posicaoTargetTokenJogador = tabuleiro.centroCasa(jogador.CasaAtual, jogador.Token);
            tokenAnimationManager.newAnimation(posicaoTargetTokenJogador, jogador);

            if (accao != null || !verificarSeDono(jogador))
            {
                cameraAnimationManager.newAnimation(Zoom.perto);
            }
        }

        private bool verificarSeDono(Jogador jogador)
        {
            bool dono = false;
            if (tabuleiro.Casa(jogador.CasaAtual) is Rua)
            {
                Rua rua = (Rua)tabuleiro.Casa(jogador.CasaAtual);
                if (rua.Dono == this.jogador)
                {
                    dono = true;
                }
            }
            return dono;
        }

        /// <summary>
        /// Move a camara um determinado numero de casas
        /// </summary>
        /// <param name="indiceCasaAtual">Indice da casa que esta atualmente ativa</param>
        /// <param name="casasAMover">Nº de casas que vamos mover</param>
        private void moverCameraNCasas(int indiceCasaOriginal, int casasAMover, Action<string> accao = null)
        {
            atualizarCasaAtual(jogador.CasaAtual);
            cameraAnimationManager.newAnimation(posicao, tabuleiro.verificarRotacaoEPartida(camera, indiceCasaOriginal, casasAMover, jogador), true);
            cameraAnimationManager.newAnimation(Zoom.perto, accao);
        }

        /// <summary>
        /// Atualiza a casa em que estamos atualmente
        /// </summary>
        /// <param name="indiceCasaAtual">Índice da casa em que estamos</param>
        private void atualizarCasaAtual(int indiceCasaAtual)
        {
            casaAtual = tabuleiro.Casa(indiceCasaAtual);
            posicao.X = casaAtual.CoordsAndSize.X - ((GraphicsDevice.Viewport.Width / 2) - (casaAtual.CoordsAndSize.Width / 2));
            posicao.Y = casaAtual.CoordsAndSize.Y - ((GraphicsDevice.Viewport.Height / 2) - (casaAtual.CoordsAndSize.Height / 2));
        }

        /// <summary>
        /// Atualiza a UI de lancamento, caso exista
        /// </summary>
        Lancamento lancamento;
        private void atualizarUILancamento()
        {
            if (UIModalAtiva != null)
            {
                if (UIModalAtiva.GetType() == typeof(UI_Lancamento))
                {
                    lancamento = UIModalAtiva.Update(random);
                    if (lancamento.somaDados != 0)
                    {
                        //A janela de lançamento produziu um valor para os dados
                        //Fechar a janela de lançamentos
                        UIModalAtiva.desativarUI(ref UIModalAtiva);
                        jogador.UltimoLancamento = lancamento.dado1 + lancamento.dado2;
                        processarDoubles(lancamento, jogador);
                    }
                }
            }
        }

        private void processarDoubles(Lancamento lancamento, Jogador jogador)
        {
            //Contar doubles e verificar se o jogador deve ir para a prisão
            if (lancamento.dado1 == lancamento.dado2)
            {
                //double!
                if (jogador.Jailed && jogador.DoubleToEscapeJail)
                {
                    jogador.Jailed = false;
                    jogador.DoubleToEscapeJail = false;
                    jogador.TurnsOnJail = 0;
                    jogador.ContadorDoubles = 0;
                    jogador.JogaOutraVez = false;
                    criarUIResultadoLancamento(lancamento);
                }
                else
                {
                    jogador.ContadorDoubles++;
                    jogador.JogaOutraVez = true;
                    if (jogador.ContadorDoubles == 3)
                    {
                        jogador.resetDoubles();
                        criarUIPrisao();
                    }
                    else
                    {
                        criarUIResultadoLancamento(lancamento);
                    }
                }

                
            }
            else
            {
                //não houve double, fazer reset ao contador de doubles
                jogador.resetDoubles();
                criarUIResultadoLancamento(lancamento);
            }
        }

        /// <summary>
        /// Implementa a lógica do jogo e altera o seu estado atual
        /// </summary>
        private void gameLogic()
        {
            switch (GameState.Estado)
            {
                case Estado.Inicial:
                    if (UIModalAtiva == null)
                    //Se existe uma janela modal ativa estamos à espera de input dos jogadores, e a lógica do jogo não avança
                    {
                        //Verificar se já existem jogadores
                        verificarListaJogadores();
                    }
                    break;
                case Estado.Lançamento:
                    //Atualizar UI de lancamento
                    atualizarUILancamento();
                    break;
                case Estado.Casa:
                    if (UIModalAtiva == null && cameraAnimationManager.getQueuedAnimations() == 0) 
                    {
                        processarCasa(casaAtual);
                    }
                    break;
                case Estado.Compra:
                    break;
                case Estado.Leilão:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Gere o processamento do código das casas
        /// </summary>
        private void processarCasa(Casa casa)
        {
            if (casaAtual is Propriedade)
            {
                processarPropriedades((Propriedade)casa);
            }
            else if (casaAtual is CommunityChest)
            {
                processarCommunityChest();
            }
            else if (casaAtual is Descanso)
            {
                //Passa apenas para o próximo jogador
                proximoJogador();
            }
            else if (casaAtual is GoTo)
            {
                goToPrison();
                
            }
            else if (casaAtual is Imposto)
            {
                processarImposto((Imposto)casa);
            }
            else if (casaAtual is Partida)
            {
                //Passa apenas para o próximo jogador
                //A lógica de receber o salário é calculada no tabuleiro/processarCasaPartida
                proximoJogador();
            }
            else if (casaAtual is Prisao)
            {
                proximoJogador();
            }
            else if (casaAtual is Sorte)
            {
                processarChance();
            }

        }

        private void processarCommunityChest()
        {
            CommunityAndChance carta = (CommunityAndChance)tabuleiro.ListaCommunity.Dequeue();
            listaOpcoes.Clear();
            opcao = new Opcao(carta.TipoOpcao == TipoOpcao.Bom? "Yeaahh!" : "Damn..", carta.TipoOpcao, true, carta.Accao);
            listaOpcoes.Add(opcao);
            texto.Clear();
            texto.Append(carta.Texto);

            criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            tabuleiro.ListaCommunity.Enqueue(carta);
        }

        private void processarChance()
        {
            CommunityAndChance carta = (CommunityAndChance)tabuleiro.ListaChance.Dequeue();
            listaOpcoes.Clear();
            opcao = new Opcao(carta.TipoOpcao == TipoOpcao.Bom ? "Yeaahh!" : "Damn..", carta.TipoOpcao, true, carta.Accao);
            listaOpcoes.Add(opcao);
            texto.Clear();
            texto.Append(carta.Texto);

            criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            tabuleiro.ListaChance.Enqueue(carta);
        }

        private void processarImposto(Imposto imposto) 
        {
            if (imposto.Percentagem == 0)
            {
                listaOpcoes.Clear();
                opcao = new Opcao("Damn..", TipoOpcao.Mau, true, (s) =>
                {
                    //Pagar o imposto associado a casa
                    jogador.pagar(imposto.CustoFixo);
                    proximoJogador();
                });
                listaOpcoes.Add(opcao);
                texto.Clear();
                texto.Append("You spend a weekend in Vegas, that costs you ");
                texto.Append(imposto.CustoFixo);

                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            }
            else 
            {
                listaOpcoes.Clear();
                opcao = new Opcao("Pay 10pc", TipoOpcao.Mau, true, (s) =>
                {
                    //Pagar o imposto de 10%
                    jogador.pagar(jogador.totalAssets() * 0.1f);
                    proximoJogador();
                });
                listaOpcoes.Add(opcao);
                opcao = new Opcao("Pay 200Euro", TipoOpcao.Mau, true, (s) =>
                {
                    jogador.pagar(imposto.CustoFixo);
                    proximoJogador();
                });
                listaOpcoes.Add(opcao);
                texto.Clear();

                texto.AppendLine("It's that time of the year!");
                texto.AppendLine("Your taxes are due.");
                texto.AppendLine();
                texto.Append("You must pay either ");
                texto.Append(imposto.CustoFixo);
                texto.Append(" or ");
                texto.Append(imposto.Percentagem);
                texto.Append("pc of your total assets.");
                
                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
                
            }


        }

        /// <summary>
        /// Processa as casas do tipo Propriedade
        /// </summary>
        private void processarPropriedades(Propriedade propriedade)
        {
            if (propriedade is Rua)
            {
                processarRua((Rua)propriedade);
            }
            else if (propriedade is Utilidade)
            {
                processarUtilidade((Utilidade)propriedade);
            }
        }

        /// <summary>
        /// Processa uma casa do tipo Utilidade (estações, eletricas, águas)
        /// </summary>
        /// <param name="utilidade">Utilidade a processar</param>
        private void processarUtilidade(Utilidade utilidade)
        {
            if (utilidade.Dono == null)
            {
                //Esta utilidade não tem dono, podemos comprá-la
                listaOpcoes.Clear();
                opcao = new Opcao("Buy!", TipoOpcao.Bom, true, (s) =>
                {
                    //Comprar a rua
                    jogador.adicionarPropriedade((Propriedade)utilidade);

                    proximoJogador();
                });
                listaOpcoes.Add(opcao);
                opcao = new Opcao("Not today.", TipoOpcao.Mau, true, (s) =>
                {
                    //Será que aqui leva a leilão??
                    proximoJogador();
                });
                listaOpcoes.Add(opcao);
                texto.Clear();
                texto.Append("You can buy ");
                texto.Append(utilidade.Nome);
                texto.Append("!");
                texto.AppendLine();
                texto.Append("For that, you pay ");
                texto.Append(utilidade.Custo);
                texto.Append(" Euro.");
                texto.Append(".");
                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            }
            else if (utilidade.Dono != null && utilidade.Dono != jogador)
            {
                //Esta utilidade tem dono e não somos nós, temos que pagar renda
                int renda;
                switch (utilidade.Tipo)
                {
                    case Tipo.Estação:
                        renda = utilidade.rendaEstacoes(utilidade.Dono.nEstacoes());
                        break;
                    case Tipo.Eletricidade:
                        renda = utilidade.rendaEletricasEAgua(utilidade.Dono.nEletricasEAguas(), jogador.UltimoLancamento);
                        break;
                    case Tipo.Água:
                        renda = utilidade.rendaEletricasEAgua(utilidade.Dono.nEletricasEAguas(), jogador.UltimoLancamento);
                        break;
                    default:
                        renda = 0;
                        break;
                }

                listaOpcoes.Clear();
                opcao = new Opcao("Damn..", TipoOpcao.Mau, true, (s) =>
                {
                    //Pagar o aluguer correspondente ao n de utilidades que o dono possui
                    jogador.pagar(renda);
                    utilidade.Dono.receber(renda);
                    proximoJogador();
                });
                listaOpcoes.Add(opcao);
                texto.Clear();
                texto.Append("You get an invoice from ");
                texto.Append(utilidade.Nome);
                texto.Append(".");
                texto.AppendLine();
                texto.Append("You must pay ");
                texto.Append(renda);
                texto.Append(" Euro to ");
                texto.Append(utilidade.Dono.Nome);
                texto.Append(".");
                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            }
            else if (utilidade.Dono == jogador)
            {
                //A utilidade já é nossa, não podemos fazer nada
                proximoJogador();
            }
        }

        /// <summary>
        /// Processa as casas do tipo Rua
        /// </summary>
        /// <param name="rua">Rua a processar</param>
        private void processarRua(Rua rua)
        {
            if (rua.Dono == null)
            {
                //Esta rua ainda não tem dono, o jogador pode comprá-la
                listaOpcoes.Clear();

                if (jogador.Dinheiro > rua.Custo)
                {
                    opcao = new Opcao("Buy!", TipoOpcao.Bom, true, (s) =>
                    {
                        //Comprar a rua
                        jogador.adicionarPropriedade((Propriedade)rua);
                        proximoJogador();
                    });
                    listaOpcoes.Add(opcao);
                }
                

                opcao = new Opcao("Not today.", TipoOpcao.Mau, true, (s) =>
                {
                    //Aqui deviamos ir para leilão.. TODO
                    proximoJogador();
                });
                listaOpcoes.Add(opcao);
                texto.Clear();
                texto.Append("You can buy ");
                texto.Append(rua.Nome);
                texto.Append("!");
                texto.AppendLine();
                texto.Append("For that, you pay ");
                texto.Append(rua.Custo);
                texto.Append(" Euro.");
                texto.Append(".");
                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            }
            else if (rua.Dono != null && rua.Dono != jogador)
            {
                
                //Esta rua tem dono e não somos nós, pagar aluguer
                int renda = 0;
                //Pagar o aluguer correspondente ao n de casas da rua
                if (tabuleiro.verificarAvenida(rua.Dono, rua.GrupoRuas))
                {
                    //Este jogador tem monopolio, a renda duplica
                    renda = rua.Renda() * 2;
                }
                else
                {
                    renda = rua.Renda();
                }

                listaOpcoes.Clear();
                opcao = new Opcao("Damn..", TipoOpcao.Mau, true, (s) =>
                {
                    
                    jogador.pagar(renda);
                    rua.Dono.receber(renda);
                    proximoJogador();
                });
                listaOpcoes.Add(opcao);
                texto.Clear();
                texto.Append("You spend the night at ");
                texto.Append(rua.Nome);
                texto.Append(".");
                texto.AppendLine();
                texto.Append("For that, you pay ");
                texto.Append(renda);
                texto.Append(" Euro to ");
                texto.Append(rua.Dono.Nome);
                texto.Append(".");
                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            }
            else if (rua.Dono == jogador)
            {
                //Somos o dono desta rua
                if (rua.NCasas < 5 
                    && tabuleiro.verificarAvenida(jogador, rua.GrupoRuas))
                {
                    listaOpcoes.Clear();

                    //Ver qual a rua deste monopolio com menos casas
                    Rua ruaMenosCasas = (Rua)tabuleiro.Casa(tabuleiro.ruaComMenosCasas(jogador.CasaAtual));

                    if (jogador.Dinheiro > ruaMenosCasas.Custo)
                    {
                        opcao = new Opcao("Build!", TipoOpcao.Bom, true, (s) =>
                        {
                            //Ver qual a rua deste monopolio com menos casas
                            
                            jogador.pagar(ruaMenosCasas.Custo);
                            ruaMenosCasas.NCasas++;
                        });
                        listaOpcoes.Add(opcao);
                    }

                    opcao = new Opcao("Not today.", TipoOpcao.Mau, true, (s) =>
                    {
                        //Não quer comprar casa, azar
                        proximoJogador();
                    });
                    listaOpcoes.Add(opcao);
                    texto.Clear();
                    texto.Append("This property is yours and you can invest in it!");
                    texto.AppendLine();
                    texto.Append("For an upgrade, you pay ");
                    texto.Append(ruaMenosCasas.Custo);
                    texto.Append(" Euro");
                    criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
                }
                else
                {
                    //Já tem 5 casas ou não tem monopólio desta rua, não pode fazer nada
                    proximoJogador();
                }
            }
        }

        /// <summary>
        /// Se não existem jogadores na lista de jogadores, cria a UI de seleção do número de jogadores
        /// </summary>
        private void verificarListaJogadores()
        {
            if (listaJogadores.Count == 0)
            {
                listaOpcoes.Clear();
                opcao = new Opcao("Two Players", TipoOpcao.Bom, true, (s) =>
                {
                    inicializarJogadores(2);
                });
                listaOpcoes.Add(opcao);
                opcao = new Opcao("Four Players", TipoOpcao.Bom, true, (s) =>
                {
                    inicializarJogadores(4);
                });
                listaOpcoes.Add(opcao);
                opcao = new Opcao("Six Players", TipoOpcao.Bom, true, (s) =>
                {
                    inicializarJogadores(6);
                });
                listaOpcoes.Add(opcao);
                opcao = new Opcao("Eight Players", TipoOpcao.Bom, true, (s) =>
                {
                    inicializarJogadores(8);
                });
                listaOpcoes.Add(opcao);
                texto.AppendLine("Welcome to Monopoly!");
                texto.AppendLine();
                texto.AppendLine("Use the buttons on the right to");
                texto.AppendLine("choose how many player will be");
                texto.AppendLine("playing.");
                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Vertical);
            }
        }

        /// <summary>
        /// Variável temporária para guardar uma UI
        /// </summary>
        UI tempUI;
        /// <summary>
        /// Inicializa a lista de jogadores com um determinado numero de jogadores,
        /// fecha a UI de selecção do número de jogadores e altera o estado do jogo
        /// </summary>
        /// <param name="numJogadores">Número de Jogadores</param>
        private void inicializarJogadores(int numJogadores)
        {
            //Criar os jogadores e inseri-los na lista
            for (int i = 0; i < numJogadores; i++)
            {
                Jogador novoJogador = new Jogador("Player " + (i + 1), (i+1), ref listaMoneyAnimation);
                novoJogador.LoadContent(Content, graphics.GraphicsDevice);
                novoJogador.Posicao = tabuleiro.centroCasa(0, novoJogador.Token);
                novoJogador.OffsetPosicao = new Vector2(random.Next(-30, 30), random.Next(-15, 30));

                listaJogadores.Add(novoJogador);
            }
            //Alterar o indice do jogador atual
            indiceJogadorAtual = 0;
            jogador = listaJogadores[indiceJogadorAtual];
            //Fechar a UI de escolha do número de jogadores
            foreach (UI ui in listaComponentesUI)
            {
                if (ui == UIModalAtiva)
                {
                    tempUI = ui;
                    ui.desativarUI(ref UIModalAtiva);
                }
            }
            listaComponentesUI.Remove(tempUI);
            //Adicionar a UI com a lista de jogadores
            loadAndAddUIJogadores("background", true, false);

            playerSplashAnimation = new PlayerAnimation(jogador, graphics.GraphicsDevice);
            
            //Zoom na casa de partida
            jogador.CasaAtual = 0;
            casasAMover = 0;
            atualizarCasaAtual(jogador.CasaAtual);

            //DEBUG
            distribuirMonopolios();


            cameraAnimationManager.newAnimation(posicao, tabuleiro.verificarRotacaoEPartida(camera, jogador.CasaAtual, casasAMover, jogador), true);
            cameraAnimationManager.newAnimation(Zoom.perto, (s) =>
            {
                //Quando as animações terminam,
                //criar uma UI de lançamento de dados e alterar estado do jogo
                GameState.Estado = Estado.Lançamento;
                criarUILancamento();
            });
        }


        /// <summary>
        /// DEBUG
        /// distribui os monopólios pelos jogadores para se poder testar o código de construir casas e hoteis
        /// </summary>
        private void distribuirMonopolios()
        {
            Jogador jogador;
            GrupoRuas grupoAnterior = GrupoRuas.Brown;
            int contador = 0;
            int indiceJogador = 0;

            jogador = listaJogadores[indiceJogador];
            foreach (Casa casa in tabuleiro.ListaCasas())
            {
                if (casa is Propriedade)
                {
                    Propriedade prop = (Propriedade)casa;
                    if (prop is Rua)
                    {
                        Rua rua = (Rua)prop;
                        if (rua.GrupoRuas == grupoAnterior)
                        {
                            jogador.adicionarPropriedade(rua);
                            jogador.receber(rua.Custo);
                        }
                        else
                        {
                            indiceJogador++;
                            if (indiceJogador > listaJogadores.Count - 1) indiceJogador = 0;
                            jogador = listaJogadores[indiceJogador];
                            jogador.adicionarPropriedade(rua);
                            jogador.receber(rua.Custo);
                        }
                        grupoAnterior = rua.GrupoRuas;
                    }
                }
            }
        }

        /// <summary>
        /// Passa para o próximo jogador a jogar
        /// </summary>
        int casaOriginal;
        private void proximoJogador()
        {
            
            GameState.Estado = Estado.Lançamento;
            casaOriginal = jogador.CasaAtual;

            if (!jogador.JogaOutraVez)
            {
                indiceJogadorAtual++;
                if (indiceJogadorAtual >= listaJogadores.Count)
                {
                    indiceJogadorAtual = 0;
                }
                jogador = listaJogadores[indiceJogadorAtual];
                playerSplashAnimation = new PlayerAnimation(jogador, graphics.GraphicsDevice);
            }
            else
            {
                jogador.JogaOutraVez = false;
            }
            

            //Mover camera para o jogador
            moverCameraCasa(casaOriginal, jogador.CasaAtual, (s) =>
            {
                //Quando as animações terminam,
                //criar uma UI de lançamento de dados e alterar estado do jogo

                if (jogador.Jailed)
                {
                    if (jogador.TurnsOnJail == 3)
                    {
                        jogador.Jailed = false;
                        jogador.TurnsOnJail = 0;
                    }
                    else
                    {
                        jogador.TurnsOnJail++;
                    }
                }

                if (!jogador.Jailed)
                {
                    criarUILancamento();
                }
                else
                {
                    criarUISairPrisao();
                }
                
            });
            
        }

        private void moverCameraCasa(int casaInicial, int casaDesejada, Action<string> accao = null)
        {
            if (casaInicial != casaDesejada)
            {
                cameraAnimationManager.newAnimation(Zoom.medio);
            }
            atualizarCasaAtual(casaDesejada);
            cameraAnimationManager.newAnimation(posicao,
                tabuleiro.verificarRotacaoEPartida(camera, casaInicial,
                    tabuleiro.nCasasDiferenca(casaInicial, casaDesejada), jogador),
                true);
            cameraAnimationManager.newAnimation(Zoom.perto, accao);
        }

        private void criarUISairPrisao()
        {
            listaOpcoes.Clear();

            if (jogador.GetOutOfJail > 0)
            {
                opcao = new Opcao("Use the card!", TipoOpcao.Bom, true, (s) =>
                {
                    jogador.GetOutOfJail--;
                    jogador.Jailed = false;
                    cameraAnimationManager.newAnimation(Zoom.medio);
                    criarUILancamento("UICentrada", true);
                });
                listaOpcoes.Add(opcao);
            }

            opcao = new Opcao("Pay $50", TipoOpcao.Mau, true, (s) =>
            {
                jogador.pagar(50);
                jogador.Jailed = false;
                cameraAnimationManager.newAnimation(Zoom.medio);
                criarUILancamento("UICentrada", true);
            });
            listaOpcoes.Add(opcao);

            opcao = new Opcao("Go for double", TipoOpcao.Mau, true, (s) =>
            {
                jogador.DoubleToEscapeJail = true;
                cameraAnimationManager.newAnimation(Zoom.medio);
                criarUILancamento("UICentrada", true);
            });
            listaOpcoes.Add(opcao);

            

            texto.Clear();
            texto.AppendLine("You're in jail!");
            texto.AppendLine();
            texto.AppendLine("Choose one of the options to get free.");
            criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
        }

        /// <summary>
        /// Gera uma janela modal de UI para lançamento de dados
        /// </summary>
        private void criarUILancamento()
        {
            
            listaOpcoes.Clear();
            opcao = new Opcao("Good luck!", TipoOpcao.Bom, true, (s) =>
            {
                cameraAnimationManager.newAnimation(Zoom.medio);
                criarUILancamento("UICentrada", true);
            });
            listaOpcoes.Add(opcao);

            texto.Clear();
            texto.AppendLine("It's your turn to play, "+ jogador.Nome +"!");
            texto.AppendLine();
            texto.AppendLine("Click the button below to roll your dice!");
            criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
        }

        private void criarUIResultadoLancamento(Lancamento lancamento)
        {

            if (jogador.Jailed && jogador.DoubleToEscapeJail)
            {
                listaOpcoes.Clear();
                opcao = new Opcao("Damn..", TipoOpcao.Mau, true, (s) =>
                {
                    proximoJogador();
                });
                listaOpcoes.Add(opcao);

                texto.Clear();
                texto.AppendLine("No double! You stay in jail.");
                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            }
            else
            {
                listaOpcoes.Clear();
                opcao = new Opcao("Ok, go!", TipoOpcao.Bom, true, (s) =>
                {
                    moverJogadorECameraNCasas(jogador.CasaAtual, lancamento.somaDados, (t) =>
                    {
                        GameState.Estado = Estado.Casa;
                    });
                });
                listaOpcoes.Add(opcao);

                texto.Clear();
                texto.AppendLine("Congratulations, you had a " + lancamento.somaDados + "!");
                texto.AppendLine();
                texto.AppendLine("Click the button below to go!");
                criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
            }
        }

        private void criarUIPrisao()
        {
            listaOpcoes.Clear();
            opcao = new Opcao("I'm innocent!", TipoOpcao.Mau, true, (s) =>
            {
                goToPrison();
            });
            listaOpcoes.Add(opcao);

            texto.Clear();
            texto.AppendLine("3 doubles in a row? You must certainly be cheating!");
            texto.AppendLine();
            texto.AppendLine("You are going to jail.");
            criarUICentrada("UICentrada", true, true, texto, listaOpcoes, OrientacaoOpcoes.Horizontal);
        }

        /// <summary>
        /// Gera uma UI centrada e corre todos os métodos necessários
        /// </summary>
        /// <param name="nomeTextura">Nome da textura</param>
        /// <param name="ativa">Se a janela está ativa</param>
        /// <param name="modal">Se a janela é modal</param>
        /// <param name="texto">Texto a inserir na janela</param>
        /// <param name="listaOpcaoes">Lista de opções a apresentar</param>
        /// <param name="orientacaoOpcoes">Orientação da lista de opções (Horizontal / Vertical)</param>
        private void criarUICentrada(string nomeTextura, bool ativa, bool modal, StringBuilder texto, List<Opcao> listaOpcaoes, OrientacaoOpcoes orientacaoOpcoes)
        {
            UI_Centrado = new UI_Centrado(nomeTextura, ativa, modal, texto, listaOpcoes, orientacaoOpcoes);
            UI_Centrado.LoadContent(Content, GraphicsDevice);
            UI_Centrado.gerarRectangulosBotoes();
            UI_Centrado.ativarUI(ref UIModalAtiva);
            listaComponentesUI.Add(UI_Centrado);
        }

        /// <summary>
        /// Cria uma UI de lançamento de dados e faz as inicializações necessárias
        /// </summary>
        /// <param name="nomeTextura">Nome da textura</param>
        /// <param name="ativa">Ativa</param>
        private void criarUILancamento(string nomeTextura, bool ativa)
        {
            UI_Lancamento = new UI_Lancamento(nomeTextura, ativa);
            UI_Lancamento.LoadContent(Content, GraphicsDevice);
            UI_Lancamento.ativarUI(ref UIModalAtiva);
            listaComponentesUI.Add(UI_Lancamento);
        }

        /// <summary>
        /// Atualiza o rectângulo do rato de acordo com a sua posição.
        /// Este rectângulo é usado para verificar se o rato intersecta um botão
        /// </summary>
        private void atualizarRectanguloRato()
        {
            rectanguloRato.X = (int)rato.Posicao.X;
            rectanguloRato.Y = (int)rato.Posicao.Y;
        }

        /// <summary>
        /// Verifica se o rato está em cima de um botão e altera o botao convenientemente
        /// </summary>
        private void verificarRatoSobreBotao()
        {
            if (UIModalAtiva != null)
            {
                //Se existe uma janela modal ativa..
                if (UIModalAtiva.getListaOpcoes() != null)
                {
                    //se a janela tem lista de opções..
                    //se temos uma janela modal aberta, apenas consideramos Hovers nessa mesma janela
                    foreach (Opcao opcao in UIModalAtiva.getListaOpcoes())
                    {
                        if (rectanguloRato.Intersects(opcao.rectangulo))
                        {
                            opcao.Hover = true;
                        }
                        else
                        {
                            if (opcao.Hover) opcao.Hover = false;
                        }
                    } 
                }
            }
            else
            {
                //não há uma janela modal aberta, temos que comparar a posicao do rato com todos os botoes de todas as janelas
                foreach (UI ui in listaComponentesUI)
                {
                    if (ui.getListaOpcoes() != null)
                    {
                        //Se este componente de UI tem uma lista de opções..
                        foreach (Opcao opcao in ui.getListaOpcoes())
                        {
                            if (rectanguloRato.Intersects(opcao.rectangulo))
                            {
                                opcao.Hover = true;
                            }
                            else
                            {
                                if (opcao.Hover) opcao.Hover = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Desenha o tabuleiro de jogo
        /// </summary>
        private void desenharTabuleiro()
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                                BlendState.AlphaBlend,
                                null,
                                null,
                                null,
                                null,
                                ViewMatrix);

            //desenhar tabuleiro
            tabuleiro.Draw(spriteBatch, camera, GraphicsDevice);
            spriteBatch.End();
        }

        /// <summary>
        /// Desenha alternadamente rectangulos por cima das casas, fazendo um efeito catita
        /// </summary>
        /// <param name="velocidade">Velocidade do piscanço, em ms</param>
        private void desenharRectangulos()
        {
            if (jogador != null)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront,
                                BlendState.AlphaBlend,
                                null,
                                null,
                                null,
                                null,
                                ViewMatrix);

                foreach (Casa casa in tabuleiro.ListaCasas())
                {
                    if (casa is Propriedade)
                    {
                        Propriedade prop = (Propriedade)casa;
                        if (prop.Dono != null && prop.Dono != jogador)
                        {
                            DrawRectangle(prop.CoordsAndSize, new Color(128, 0, 0, 5));
                        }
                        else if (prop.Dono != null && prop.Dono == jogador)
                        {
                            DrawRectangle(prop.CoordsAndSize, new Color(0, 128, 0, 5));
                        }
                    }
                }
                spriteBatch.End();
            }
            
        }

        /// <summary>
        /// Variável temporaria utilizada para guadar a lista de componentes de UI antes de a percorrer,
        /// uma vez que pode estar a ser alterada por outros métodos
        /// O 100 está martelado porque nunca vamos ter 100 janelas de UI ao mesmo tempo a menos que algo tenha corrido muito mal
        /// </summary>
        UI[] tempListaComponentesUI = new UI[100];
        /// <summary>
        /// Lida com descliques do rato, passando todos os botões que estavamn clicados para o seu estado normal
        /// </summary>
        /// <param name="clique"></param>
        private void processarDescliquesRato(Clique desclique)
        {
            listaComponentesUI.CopyTo(tempListaComponentesUI);
            foreach (UI ui in tempListaComponentesUI)
            {
                if (ui != null)
                {
                    if (ui.getListaOpcoes() != null)
                    {
                        //Se este componente de UI tem lista de opções..
                        foreach (Opcao opcao in ui.getListaOpcoes())
                        {
                            if (rectanguloRato.Intersects(opcao.rectangulo) && opcao.Activa)
                            {
                                rato.Blocked = true;
                                if (opcao.CloseOnClick)
                                {
                                    opcao.Activa = false;
                                    ui.desativarUI(ref UIModalAtiva);
                                }
                                opcao.ExecutarAccao();
                                rato.Blocked = false;
                            }
                            opcao.Clique = false;
                        }
                    }
                }
                else
                {
                    //Esta UI foi desligada aquando do click num botão que a fecha, ignorar
                }
            }
        }

        /// <summary>
        /// Variável reutilizada para guardar uma lista de UIs
        /// </summary>
        List<UI> tempListaUI = new List<UI>();
        /// <summary>
        /// Percorre a lista de componentes de UI e remove as UIs desativas
        /// </summary>
        private void eliminarUIsDesativas()
        {
            tempListaUI.Clear();
            foreach (UI ui in listaComponentesUI)
            {
                if (!ui.Ativa)
                {
                    tempListaUI.Add(ui);
                }
            }
            foreach (UI ui in tempListaUI)
            {
                listaComponentesUI.Remove(ui);
            }
        }

        /// <summary>
        /// Lida com os eventos disparados pelo rato
        /// </summary>
        /// <param name="posicao">Posicao do rato quando aconteceu o clique</param>
        private void processarCliquesRato(Clique clique)
        {
            if (UIModalAtiva != null)
            {
                if (UIModalAtiva.getListaOpcoes() != null)
                {
                    //se temos uma janela modal aberta, apenas consideramos Hovers nessa mesma janela
                    foreach (Opcao opcao in UIModalAtiva.getListaOpcoes())
                    {
                        if (rectanguloRato.Intersects(opcao.rectangulo))
                        {
                            opcao.Clique = true;
                        }
                        else
                        {
                            if (opcao.Clique) opcao.Clique = false;
                        }
                    } 
                }
            }
            else
            {
                //não há uma janela modal aberta, temos que comparar a posicao do rato com todos os botoes de todas as janelas
                foreach (UI ui in listaComponentesUI)
                {

                    if (ui.getListaOpcoes() != null)
                    {
                        //Esta UI tem uma lista de opções..
                        foreach (Opcao opcao in ui.getListaOpcoes())
                        {
                            if (rectanguloRato.Intersects(opcao.rectangulo))
                            {
                                opcao.Clique = true;
                            }
                            else
                            {
                                if (opcao.Clique) opcao.Clique = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Desenha as coordenadas atuais do rato
        /// Útil para debug
        /// </summary>
        /// <param name="rato">Uma instância do rato</param>
        private void desenharCoordenadasRato(Rato rato)
        {
            spriteBatch.DrawString(arial12, rato.Posicao.ToString(), new Vector2(rato.Posicao.X, rato.Posicao.Y), Color.Black);
        }

        /// <summary>
        /// Desenhar as coordenadas da camera junto ao rato
        /// Útil para debug
        /// </summary>
        /// <param name="camera">Uma instância da camera</param>
        private void desenharCoordenadasCamera(Camera camera)
        {
            spriteBatch.DrawString(arial12, camera.Posicao.ToString(), new Vector2(rato.Posicao.X, rato.Posicao.Y), Color.White);
        }

        /// <summary>
        /// Desenha a rotação da camera (em radianos) junto ao rato
        /// Útil para debug
        /// </summary>
        /// <param name="camera">Uma instância da camera</param>
        private void desenharRotacaoCamera(Camera camera)
        {
            spriteBatch.DrawString(arial12, camera.getRotacao().ToString(), new Vector2(rato.Posicao.X + 20, rato.Posicao.Y), Color.White);
        }

        /// <summary>
        /// Define as camadas de background parallax
        /// </summary>
        private void gerarCamadasParallax()
        {
            layers = new List<ParallaxLayer>
            {
                new ParallaxLayer(camera) { parallax = new Vector2(0.4f, 0.4f) },
                new ParallaxLayer(camera) { parallax = new Vector2(0.7f, 0.7f) },
                new ParallaxLayer(camera) { parallax = new Vector2(0.6f, 0.6f) },
                new ParallaxLayer(camera) { parallax = new Vector2(0.5f, 0.5f) },
            };


            Sprite sprite;

            sprite = new Sprite("city2",new Vector2(256, 256), Vector2.Zero);
            loadAndAddParallax(0, sprite);

            sprite = new Sprite("nuvens1", new Vector2(-1024, 350), new Vector2(-0.5f, 0));
            loadAndAddParallax(1, sprite);

            sprite = new Sprite("nuvens2", new Vector2(1024 * 2, 350), new Vector2(-0.7f, 0));
            loadAndAddParallax(2, sprite);

            sprite = new Sprite("nuvens3", new Vector2(1024 * 3, 350), new Vector2(-1f, 0));
            loadAndAddParallax(3, sprite);
        }

        /// <summary>
        /// Load das texturas necessárias para os backgrounds parallax
        /// </summary>
        /// <param name="indice">Indice da camada a que o background pertence</param>
        /// <param name="sprite">Sprite a adicionar à camada</param>
        private void loadAndAddParallax(int indice, Sprite sprite)
        {
            sprite.LoadContent(Content, GraphicsDevice);
            layers[indice].sprites.Add(sprite);
        }

        /// <summary>
        /// Cria uma UI com a lista de jogadores e algumas características destes
        /// </summary>
        /// <param name="nomeTextura">Nome da textura a utilizar</param>
        /// <param name="ativa">Ativa</param>
        /// <param name="modal">Modal</param>
        private void loadAndAddUIJogadores(string nomeTextura, bool ativa, bool modal)
        {
            UI_Jogadores jogadores = new UI_Jogadores(nomeTextura, modal);
            jogadores.LoadContent(Content, GraphicsDevice);
            if (ativa)
            {
                jogadores.ativarUI(ref UIModalAtiva);
            }
            listaComponentesUI.Add(jogadores);
        }

        private void loadAndAddUIPropriedadesJogador(string nomeTextura, bool ativa, bool modal)
        {
            
        
        }

        /// <summary>
        /// Desenha todos os componentes de UI que estão ativas
        /// </summary>
        private void desenharUI()
        {
            foreach (UI ui in listaComponentesUI)
            {
                if(ui.Ativa)
                    ui.Draw(spriteBatch, camera, arial12, listaJogadores, tabuleiro, jogador);
            }
        }

        /// <summary>
        /// Atualiza as camadas de parallax que se movem (nuvens)
        /// </summary>
        private void atualizarCamadasParallax()
        {
            foreach (ParallaxLayer layer in layers)
            {
                for (int i = 0; i < layer.sprites.Count; i++)
                {
                    layer.sprites[i].Update();
                }
            }
        }

        /// <summary>
        /// Desenha um rectângulo numa determinada posicao e com uma dada cor
        /// </summary>
        /// <param name="coords">Posição onde o rectângulo será desenhado</param>
        /// <param name="color">Cor</param>
        private void DrawRectangle(Rectangle coords, Color color)
        {
            Texture2D debugRectangle = new Texture2D(GraphicsDevice, 1, 1);   
            debugRectangle.SetData(new[] { color });
            spriteBatch.Draw(debugRectangle, coords, color);
        }

        #endregion
    }
}
