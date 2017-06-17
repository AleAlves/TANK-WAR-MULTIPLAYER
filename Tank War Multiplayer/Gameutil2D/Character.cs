using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gameutil2d.classes.basic;
using gameutil2d.classes.scene;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


/*
 *  [Character.cs]  - Classe destinada para a construção de personagens, voltado para jogos estilo plataforma
 *  Possui recursos de detecção de colisão, processamento de física (pulo e queda) e etc.
 * 
 *  Desenvolvida por : Luciano Alves da Silva
 *  e-mail : lucianopascal@yahoo.com.br
 *  site : http://www.gameutil2d.org
 *  
 */

namespace gameutil2d.classes.character
{
    class Character : GameElement
    {
        bool invert;

        AnimationSprites aCurrentAnimation;

        enum AnimationType { IDLE, MOVE, ATTACK, JUMPING, DAMAGED, DEAD }

        enum ActionState { NONE, IDLE, MOVE, ATTACK, DAMAGED, DEAD }

        enum DirectionState { RIGHT, LEFT }

        enum JumpState { JUMPING, FALLING, IS_GROUND }

        enum DamageState { NO_DAMAGE, DAMAGED }

        enum LiveState { LIVE, DYING, DEAD }

        List<String> aAnimationName;
        List<AnimationSprites> aAnimations;
        List<AnimationType> aAnimationType;

        ActionState action;

        DirectionState direction;

        JumpState jump;

        DamageState damageState;

        LiveState liveState;

        List<AnimationSprites> animationList;



        List<String> aCollisionElementBySide_Tag;

        List<String> aCollisionElementByFall_Tag;




        int INITIAL_VELOCITY_JUMP = -18;

        int MAX_VELOCITY_FALL = 12;

        int JumpShift;

        int MAX_FRAME_DAMAGE = 15;

        int countFrameDamage;

        bool enableFall = true;

        ContentManager content;


        int last_idle_animation_frame;
        int last_jumping_animation_frame;
        bool last_loop_idle_animation;
        bool last_loop_jumping_animation;

        String idle_animation_name_after_jumping, idle_animation_name_after_attack;

        public Character(ContentManager content, int x, int y, int w, int h)
        {
            base.SetBounds(x, y, w, h);

            this.content = content;

            aAnimationName = new List<String>();
            aAnimations = new List<AnimationSprites>();
            aAnimationType = new List<AnimationType>();

            aCollisionElementBySide_Tag = new List<String>();


            aCollisionElementByFall_Tag = new List<String>();


            direction = DirectionState.RIGHT;
            jump = JumpState.IS_GROUND;
            damageState = DamageState.NO_DAMAGE;

            invert = false;

            idle_animation_name_after_jumping = null;
            idle_animation_name_after_attack = null;
            last_loop_idle_animation = false;
            last_loop_jumping_animation = false;
        }


        private void AddNewSprite(String animation_name, string path_name, AnimationType animationType)
        {
            //Primeiramente, será feito uma busca no array "aAnimationName" para checar
            //se a animação já existe
            int index = aAnimationName.IndexOf(animation_name);
            if (index != -1)
            {
                //Se existir, adiciona a imagem no array de imagem na posição referenciada por index
                aAnimations[index].Add(path_name);

            }
            else
            {
                //Se a animação não existe, adiciona o nome da animação no vetor de nomes de animações
                aAnimationName.Add(animation_name);
                //Cria no vetor de animação, uma nova animação, referenciada por  nome acima			
                AnimationSprites animation = new AnimationSprites(content, GetX(), GetY(), GetWidth(), GetHeight());
                animation.Add(path_name);
                aAnimations.Add(animation);
                //Adiciona o tipo da animação
                aAnimationType.Add(animationType);
            }
        }

        public void AddNewSpriteIdle(String animation_name, params string[] path_names)
        {
            foreach (string path_name in path_names)
                AddNewSprite(animation_name, path_name, AnimationType.IDLE);
        }

        public void AddNewSpriteMove(String animation_name, params string[] path_names)
        {
            foreach (string path_name in path_names)
                AddNewSprite(animation_name, path_name, AnimationType.MOVE);
        }

        public void AddNewSpriteJumping(String animation_name, params string[] path_names)
        {
            foreach (string path_name in path_names)
                AddNewSprite(animation_name, path_name, AnimationType.JUMPING);
        }

        public void AddNewSpriteAttack(String animation_name, params string[] path_names)
        {
            foreach (string path_name in path_names)
                AddNewSprite(animation_name, path_name, AnimationType.ATTACK);
        }

        public void AddNewSpriteDamage(String animation_name, params string[] path_names)
        {
            foreach (string path_name in path_names)
                AddNewSprite(animation_name, path_name, AnimationType.DAMAGED);
        }

        public void AddNewSpriteDie(String animation_name, params string[] path_names)
        {
            foreach (string path_name in path_names)
                AddNewSprite(animation_name, path_name, AnimationType.DEAD);
        }

        public void Idle(String animation_name, int frames, bool loop)
        {
            if (action != ActionState.IDLE)
            {
                action = ActionState.IDLE;
                //Busca pelo vetor de nomes de animação, se existe a animação informada
                int index = aAnimationName.IndexOf(animation_name);

                if (index != -1)
                {
                    last_idle_animation_frame = frames;
                    last_loop_idle_animation = loop;

                    //Verifica se a animação encontrada é uma animação do tipo Idle
                    if (aAnimationType[index] == AnimationType.IDLE)
                    {
                        aCurrentAnimation = aAnimations[index];
                        //Executa a animação
                        aCurrentAnimation.Start(frames, loop);
                    }
                }
            }
        }

        public void Idle(int frames, bool loop)
        {
            if (action != ActionState.IDLE)
            {
                action = ActionState.IDLE;
                //Como não foi informado o nome da animação do personagem parado,
                //exibir a primeira animação que representa o personagem parado
                int index = aAnimationType.IndexOf(AnimationType.IDLE);

                if (index != -1)
                {
                    last_idle_animation_frame = frames;
                    last_loop_idle_animation = loop;

                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, loop);
                }


            }
        }

        private void ForceIdle(int frames, bool loop)
        {
            action = ActionState.IDLE;
            //Como não foi informado o nome da animação do personagem parado,
            //exibir a primeira animação que representa o personagem parado
            int index = aAnimationType.IndexOf(AnimationType.IDLE);

            if (index != -1)
            {
                last_idle_animation_frame = frames;
                last_loop_idle_animation = loop;

                aCurrentAnimation = aAnimations[index];
                aCurrentAnimation.Start(frames, loop);
            }
        }

        public void MoveToRight(String animation_name, int frames, bool loop)
        {
            if ((direction != DirectionState.RIGHT) || (action != ActionState.MOVE))
            {
                direction = DirectionState.RIGHT;
                action = ActionState.MOVE;

                int index = aAnimationName.IndexOf(animation_name);

                if (index != -1)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, loop);
                }

            }

        }

        public void MoveToRight(int frames, bool loop)
        {
            if ((direction != DirectionState.RIGHT) || (action != ActionState.MOVE))
            {
                direction = DirectionState.RIGHT;
                action = ActionState.MOVE;

                int index = aAnimationType.IndexOf(AnimationType.MOVE);

                if (index != -1)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, loop);
                }

            }

        }

        public void MoveToLeft(String animation_name, int frames, bool loop)
        {
            if ((direction != DirectionState.LEFT) || (action != ActionState.MOVE))
            {
                direction = DirectionState.LEFT;
                action = ActionState.MOVE;

                int index = aAnimationName.IndexOf(animation_name);

                if (index != -1)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, loop);
                }
            }

        }

        public void MoveToLeft(int frames, bool loop)
        {
            if ((direction != DirectionState.LEFT) || (action != ActionState.MOVE))
            {
                direction = DirectionState.LEFT;
                action = ActionState.MOVE;

                int index = aAnimationType.IndexOf(AnimationType.MOVE);

                if (index != -1)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, loop);
                }
            }

        }

        public void Attack(String animation_name, int frames)
        {

            action = ActionState.ATTACK;

            int index = aAnimationName.IndexOf(animation_name);

            if (index != -1)
            {
                aCurrentAnimation = aAnimations[index];
                aCurrentAnimation.Start(frames, false);
                idle_animation_name_after_attack = null;
            }

        }

        public void Attack(int frames)
        {

            action = ActionState.ATTACK;

            int index = aAnimationType.IndexOf(AnimationType.ATTACK);

            if (index != -1)
            {
                aCurrentAnimation = aAnimations[index];
                aCurrentAnimation.Start(frames, false);
                idle_animation_name_after_attack = null;
            }

        }

        public void Attack(String animation_name, int frames, String idle_animation_name_after_attack)
        {

            action = ActionState.ATTACK;

            int index = aAnimationName.IndexOf(animation_name);

            if (index != -1)
            {
                aCurrentAnimation = aAnimations[index];
                aCurrentAnimation.Start(frames, false);
                this.idle_animation_name_after_attack = idle_animation_name_after_attack;
            }

        }

        public void Attack(int frames, String idle_animation_name_after_attack)
        {

            action = ActionState.ATTACK;

            int index = aAnimationType.IndexOf(AnimationType.ATTACK);

            if (index != -1)
            {
                aCurrentAnimation = aAnimations[index];
                aCurrentAnimation.Start(frames, false);
                this.idle_animation_name_after_attack = idle_animation_name_after_attack;
            }

        }

        public void Jump(String animation_name, int frames, bool loop)
        {
            if (jump == JumpState.IS_GROUND)
            {
                jump = JumpState.JUMPING;
                JumpShift = INITIAL_VELOCITY_JUMP;

                int index = aAnimationName.IndexOf(animation_name);

                if (index != -1)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, false);
                }
            }
        }

        public void Jump(int frames, bool loop)
        {
            if (jump == JumpState.IS_GROUND)
            {
                jump = JumpState.JUMPING;
                JumpShift = INITIAL_VELOCITY_JUMP;

                int index = aAnimationType.IndexOf(AnimationType.JUMPING);

                if (index != -1)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, false);
                }
            }
        }

        public void Jump(String animation_name, int frames, int jump_shift, bool loop)
        {
            if (jump == JumpState.IS_GROUND)
            {
                jump = JumpState.JUMPING;
                JumpShift = jump_shift;

                int index = aAnimationName.IndexOf(animation_name);

                if (index != -1)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, false);
                }
            }
        }

        public void Jump(int frames, int jump_shift, bool loop)
        {
            if (jump == JumpState.IS_GROUND)
            {
                jump = JumpState.JUMPING;
                JumpShift = jump_shift;

                int index = aAnimationType.IndexOf(AnimationType.JUMPING);

                if (index != -1)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, false);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (damageState == DamageState.DAMAGED)
            {


                countFrameDamage++;

                if (countFrameDamage != MAX_FRAME_DAMAGE)
                {
                    if ((countFrameDamage % 2) == 0)
                        return;
                }
                else
                {
                    countFrameDamage = 0;
                    damageState = DamageState.NO_DAMAGE;
                }
            }

            if (!invert)
            {
                if (direction == DirectionState.RIGHT)
                    aCurrentAnimation.Draw(spriteBatch);
                else
                    aCurrentAnimation.Draw(spriteBatch, true);

            }
            else
            {

                if (direction == DirectionState.RIGHT)
                    aCurrentAnimation.Draw(spriteBatch, true);
                else
                    aCurrentAnimation.Draw(spriteBatch);

            }
        }

        private bool IsCollisionElementByFall(GameElement element)
        {

            bool isElement = false;

            {

                foreach (String tag in aCollisionElementByFall_Tag)
                {
                    if (element.GetTag() == tag)
                    {
                        isElement = true;

                        break;
                    }
                }

                if (isElement)
                    return true;

            }

            return isElement;

        }


        private bool IsCollisionElementBySide(GameElement element)
        {

            bool isElement = false;


            {

                foreach (String tag in aCollisionElementBySide_Tag)
                {
                    if (element.GetTag() == tag)
                        isElement = true;
                }

                if (isElement)
                    return true;


            }

            return isElement;

        }



        public void Update(Scene scene)
        {


            bool isGround = false;



            if (action == ActionState.ATTACK)
            {
                if (!aCurrentAnimation.IsPlaying())
                {
                    action = ActionState.IDLE;

                    if (jump == JumpState.IS_GROUND)
                    {
                        int index;

                        if (idle_animation_name_after_attack != null)
                        {
                            index = aAnimationName.IndexOf(idle_animation_name_after_attack);
                            aCurrentAnimation = aAnimations[index];
                            aCurrentAnimation.Start(last_idle_animation_frame, last_loop_idle_animation);

                        }
                        else
                        {
                            index = aAnimationType.IndexOf(AnimationType.IDLE);
                            aCurrentAnimation = aAnimations[index];
                            aCurrentAnimation.Start(last_idle_animation_frame, last_loop_idle_animation);
                        }


                    }
                    else
                    { //Está caindo ???

                        int index = aAnimationType.IndexOf(AnimationType.JUMPING);
                        aCurrentAnimation = aAnimations[index];
                        aCurrentAnimation.Start(last_jumping_animation_frame, last_loop_jumping_animation);

                    }

                }



            }

            else if (action == ActionState.DAMAGED)
            {
                if (!aCurrentAnimation.IsPlaying())
                {
                    action = ActionState.IDLE;

                    if (jump == JumpState.IS_GROUND)
                    {
                        int index;

                        if (idle_animation_name_after_attack != null)
                            index = aAnimationName.IndexOf(idle_animation_name_after_attack);
                        else
                            index = aAnimationType.IndexOf(AnimationType.IDLE);

                        aCurrentAnimation = aAnimations[index];
                        aCurrentAnimation.Start(last_idle_animation_frame, last_loop_idle_animation);
                    }
                    else
                    { //Está caindo ???

                        int index = aAnimationType.IndexOf(AnimationType.JUMPING);
                        aCurrentAnimation = aAnimations[index];
                        aCurrentAnimation.Start(last_jumping_animation_frame, last_loop_jumping_animation);

                    }
                }
            }
            else if (action == ActionState.DEAD)
            {

                if (aCurrentAnimation.IsPlaying())
                {
                    liveState = LiveState.DYING;
                }
                else
                {
                    liveState = LiveState.DEAD;
                }

            }

            if (!enableFall)
                return; //Sai, não será processado nenhuma queda

            if (jump == JumpState.JUMPING)
            {


                this.MoveByY(JumpShift);


                JumpShift++;

                if (JumpShift == 0)
                {
                    jump = JumpState.FALLING;

                }
            }
            else if (jump == JumpState.FALLING)
            {

                this.MoveByY(JumpShift);

                JumpShift++;

                if (JumpShift > MAX_VELOCITY_FALL)
                    JumpShift--;



                //Processa todos os elementos da tela para ver se colidiu com o alguma coisa
                foreach (GameElement element in scene.Elements())
                {

                    bool colidiu = false;

                    if ((IsCollisionElementByFall(element)) || (IsCollisionElementBySide(element)))
                    {


                        //Checa a colisao entre os objetos
                        if (Collision.Check(this, element))
                        {



                            if ((this.GetY() + this.GetHeight()) <= (element.GetY() + 15))
                            {



                                jump = JumpState.IS_GROUND;



                                this.SetY(element.GetY() - (this.GetHeight()));

                                colidiu = true;

                            }

                        }

                    }

                    if (colidiu)
                    {
                        ForceIdle(last_idle_animation_frame, last_loop_idle_animation);
                        break;
                    }

                }


            }
            else if (jump == JumpState.IS_GROUND)
            {

                foreach (GameElement element in scene.Elements())
                {

                    if ((IsCollisionElementByFall(element)) || (IsCollisionElementBySide(element)))
                    {


                        if (


                                ((this.GetY() + (this.GetHeight())) == (element.GetY())) &&

                                 ((this.GetX() + (this.GetWidth())) >= element.GetX()) &&

                                 ((this.GetX()) <= (element.GetX() + element.GetWidth())))
                        {

                            isGround = true;

                            break;
                        }


                    }


                }

                if (!isGround)
                {
                    Jump(last_jumping_animation_frame, last_loop_jumping_animation);
                    jump = JumpState.FALLING;
                    JumpShift = 0;

                }

            }



        }


        public bool IsAttacking()
        {
            return (action == ActionState.ATTACK);
        }

        public bool IsDamaged()
        {
            return (damageState == DamageState.DAMAGED);
        }


        public bool IsDying()
        {
            return (liveState == LiveState.DYING);
        }

        public bool IsDead()
        {
            return (liveState == LiveState.DEAD);
        }

        public bool IsGround()
        {
            return (jump == JumpState.IS_GROUND);
        }


        public void SufferDamage(String animation_name, int frames)
        {
            countFrameDamage = 0;
            damageState = DamageState.DAMAGED;
            action = ActionState.DAMAGED;

            int index = aAnimationName.IndexOf(animation_name);

            if (index != -1)
            {
                AnimationType atype = aAnimationType[index];
                if (atype == AnimationType.DAMAGED)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, false);
                }
            }
        }

        public void SufferDamage(int frames)
        {
            countFrameDamage = 0;
            damageState = DamageState.DAMAGED;
            action = ActionState.DAMAGED;

            int index = aAnimationType.IndexOf(AnimationType.DAMAGED);

            if (index != -1)
            {
                if (aAnimationType[index] == AnimationType.DAMAGED)
                {
                    aCurrentAnimation = aAnimations[index];
                    aCurrentAnimation.Start(frames, false);
                }
            }
        }


        public void Die(String animation_name, int frames)
        {

            action = ActionState.DEAD;

            int index = aAnimationName.IndexOf(animation_name);

            if (index != -1)
            {
                aCurrentAnimation = aAnimations[index];
                aCurrentAnimation.Start(frames, false);
                idle_animation_name_after_attack = null;
            }

        }

        public void Die(int frames)
        {

            action = ActionState.DEAD;

            int index = aAnimationType.IndexOf(AnimationType.DEAD);

            if (index != -1)
            {
                aCurrentAnimation = aAnimations[index];
                aCurrentAnimation.Start(frames, false);
                idle_animation_name_after_attack = null;
            }

        }

        public void SetMaxFramesDamageDuration(int max_frames)
        {
            MAX_FRAME_DAMAGE = max_frames;
        }


        public void AddCollisionElementOfFallByTag(String tag)
        {
            aCollisionElementByFall_Tag.Add(tag);
        }

        public void AddCollisionElementOfSideByTag(String tag)
        {
            aCollisionElementBySide_Tag.Add(tag);
        }

        public override void MoveByX(int value)
        {


            base.MoveByX(value);

            foreach (AnimationSprites a in aAnimations)
                a.MoveByX(value);

        }

        public override void MoveByY(int value)
        {


            base.MoveByY(value);

            foreach (AnimationSprites a in aAnimations)
                a.MoveByY(value);

        }

        public override void SetX(int value)
        {


            base.SetX(value);

            foreach (AnimationSprites a in aAnimations)
                a.SetX(value);

        }

        public override void SetY(int value)
        {


            base.SetY(value);

            foreach (AnimationSprites a in aAnimations)
                a.SetY(value);

        }

        public override void SetWidth(int value)
        {


            base.SetWidth(value);

            foreach (AnimationSprites a in aAnimations)
                a.SetWidth(value);

        }

        public override void SetHeight(int value)
        {


            base.SetHeight(value);

            foreach (AnimationSprites a in aAnimations)
                a.SetHeight(value);

        }

        public bool CollisionBySide(Scene scene)
        {
            bool anyCollision = false;

            foreach (GameElement e in scene.Elements())
            {
                if (IsCollisionElementBySide(e))
                {
                    if ((Collision.Check(this, e)) && ((this.y + this.height) >= e.GetY() + 10))
                    {
                        anyCollision = true;
                        break;
                    }
                }
            }

            return anyCollision;
        }

        public void SetBounds(int x, int y, int w, int h)
        {

            SetY(x);
            SetY(y);
            SetWidth(w);
            SetHeight(h);

        }

        public void SetEnableJumpAndFall(bool jump_fall)
        {
            enableFall = jump_fall;
        }

        public void TurnToLeft()
        {
            direction = DirectionState.LEFT;
        }

        public void TurnToRight()
        {
            direction = DirectionState.RIGHT;
        }

        public void InvertSprites()
        {
            invert = !invert;
        }


    }
}
