using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;


namespace Test.PlayMode
{
    public class PlayerMovementTests
    {
        [SetUp]
        public void SetUp()
        {
            EditorSceneManager.LoadScene("Assets/Scenes/SampleScene.unity");
        }
        
        [UnityTest]
        public IEnumerator IsGrounded_ReturnsTrue_WhenPlayerIsOnGround()
        {
            // Arrange
            
            var player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            var playerObject = GameObject.Instantiate(player);
            Debug.Log(playerObject);
            if (playerObject != null)
            {
                PlayerMovement playerMovement = playerObject.GetComponent<PlayerMovement>();
                playerMovement.coll = playerObject.GetComponent<BoxCollider2D>();
                playerMovement.rb = playerObject.GetComponent<Rigidbody2D>();
                playerMovement.jumpableGround = LayerMask.GetMask("Ground"); // Assuming your ground layer is named "Ground".
                yield return null;
                Assert.IsNotNull(playerMovement.jumpableGround, "jumplableGround not set on PlayerMovement component");
                if (playerMovement != null)
                { 
                    // Act
                    
                    var isGrounded = playerMovement.IsGrounded();

                    // Assert
                    Assert.IsTrue(isGrounded, "Expected the player to be grounded");
                }
                else
                {
                    Assert.Fail("PlayerMovement component not found on player object");
                }
                
            }
            else
            {
                Assert.Fail("Failed to instantiate the player object");
            }
            // Clean up
            GameObject.DestroyImmediate(playerObject);
        }

        [Test]
        public void IsGrounded_ReturnsFalse_WhenPlayerIsNotOnGround()
        {
            // Arrange
            var player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            var playerObject = GameObject.Instantiate(player);
            var playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerMovement.coll = playerObject.GetComponent<BoxCollider2D>();
            playerMovement.rb = playerObject.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(playerMovement, "PlayerMovement component not found on player object");

            playerMovement.jumpableGround = LayerMask.GetMask("Ground"); // Assuming your ground layer is named "Ground".

            // Simulate player being in the air
            playerObject.transform.position = new Vector3(0f, 2f, 0f);

            // Act
            var isGrounded = playerMovement.IsGrounded();

            // Assert
            Assert.IsNotNull(playerObject);
            Assert.IsFalse(isGrounded, "Expected the player not to be grounded");

            // Clean up
            GameObject.DestroyImmediate(playerObject);
        }

        [UnityTest]
        public IEnumerator IsWall_ReturnsTrue_WhenPlayerIsOnWall()
        {
            // Arrange
            var player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            var playerObject = GameObject.Instantiate(player);
            var playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerMovement.coll = playerObject.GetComponent<BoxCollider2D>();
            playerMovement.rb = playerObject.GetComponent<Rigidbody2D>();
            playerMovement.jumpableGround = LayerMask.GetMask("Ground"); // Assuming your ground layer is named "Ground".
            playerMovement.Run(1f);
            yield return null; 
            while (playerMovement.transform.position.x < 3f)
            {
                // Esperar hasta que la posición x deje de cambiar
                yield return null;
            }
            var isWall = playerMovement.IsWall();
            Assert.IsTrue(isWall, "Expected the player to be against a wall");
            // Clean up
            GameObject.DestroyImmediate(playerObject);
        }

        [Test]
        public void IsWall_ReturnsFalse_WhenPlayerIsNotOnWall()
        {
            // Arrange
            var player = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            var playerObject = GameObject.Instantiate(player);
            var playerMovement = playerObject.GetComponent<PlayerMovement>();
            playerMovement.coll = playerObject.GetComponent<BoxCollider2D>();
            playerMovement.rb = playerObject.GetComponent<Rigidbody2D>();
            playerMovement.jumpableGround = LayerMask.GetMask("Ground"); // Assuming your ground layer is named "Ground".
            
            var isWall = playerMovement.IsWall();
            Assert.IsFalse(isWall, "Expected the player to be against a wall");
            // Clean up
            GameObject.DestroyImmediate(playerObject);
        }
    }
}



