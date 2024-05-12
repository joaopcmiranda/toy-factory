using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class PlayerManager : MonoBehaviour
    {

        public List<Character> characters;

        private int _activeCharacterIndex = 0;
        private Character _activeCharacter;
        private bool _isMultiCharacter = false;

        // Start is called before the first frame update
        void Start()
        {
            if(characters.Count == 0)
            {
                throw new System.Exception("No characters found in PlayerManager. Please add at least one character.");
            }else if (characters.Count == 1)
            {
                _activeCharacter = characters[0];
                _activeCharacter.active = true;
            }
            if (characters.Count > 1)
            {
                _isMultiCharacter = true;

                // deactivate all characters except the first one
                for (var i = 1; i < characters.Count; i++)
                {
                    characters[i].active = false;
                }
                _activeCharacter = characters[_activeCharacterIndex];
                _activeCharacter.active = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Change character when Tab is pressed
            if (_isMultiCharacter && Input.GetKeyDown(KeyCode.Tab))
            {
                _activeCharacter.active = false;
                _activeCharacterIndex = (_activeCharacterIndex + 1) % characters.Count;
                _activeCharacter = characters[_activeCharacterIndex];
                _activeCharacter.active = true;
            }
        }
    }

}
