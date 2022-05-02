using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Customization : MonoBehaviour
{
    public GameObject maleModel;
    public GameObject femaleModel;
    public GameObject hairSliderMale;
    public GameObject hairSliderFemale;
    public List<Material> hairMaterials;
    public List<GameObject> femaleHeadModels;
    public List<GameObject> maleHeadModels;
    public List<SkinnedMeshRenderer> hairRenderers; 
    public List<SkinnedMeshRenderer> bodyParts;
    public List<SkinnedMeshRenderer> shirts;
    public List<SkinnedMeshRenderer> pants;
    public List<Material> clothingMaterials;
    public List<Material> skinMaterials;
    public string sceneName;

    public bool isMale = true;

    public void SetFemale() 
    {
        maleModel.SetActive(false);
        femaleModel.SetActive(true);
        hairSliderFemale.SetActive(true);
        hairSliderMale.SetActive(false);
        isMale = false;
    }

    public void SetMale()
    {
        maleModel.SetActive(true);
        femaleModel.SetActive(false);
        hairSliderFemale.SetActive(false);
        hairSliderMale.SetActive(true);
        isMale = true;
    }

    public void SlideSkin(float value)
    {
        for (int i = 0; i < bodyParts.Count - 2; i++)
        {
            Material[] materialsArr = bodyParts[i].materials;
            materialsArr[0] = skinMaterials[(int)value];
            bodyParts[i].materials = materialsArr; 
        }
        for (int i = 7; i < bodyParts.Count; i++)
        {
            Material[] materialsArr = bodyParts[i].materials;
            materialsArr[1] = skinMaterials[(int)value];
            bodyParts[i].materials = materialsArr; 
        }
    }

    public void SlideHairMale(float value)
    {
        for (int i = 0; i < maleHeadModels.Count; i++) 
        {
            maleHeadModels[i].SetActive(false);
        }
        maleHeadModels[(int)value].SetActive(true);
    }

    public void SlideHairFemale(float value)
    {
        for (int i = 0; i < femaleHeadModels.Count; i++) 
        {
            femaleHeadModels[i].SetActive(false);
        }
        femaleHeadModels[(int)value].SetActive(true);
    }

    public void SlideHairColor(float value)
    {
        Material[] materialsArr = hairRenderers[0].materials;
        materialsArr[1] = hairMaterials[(int)value];
        hairRenderers[0].materials = materialsArr; 

        materialsArr = hairRenderers[1].materials;
        materialsArr[1] = hairMaterials[(int)value];
        hairRenderers[1].materials = materialsArr;

        materialsArr = hairRenderers[2].materials;
        materialsArr[1] = hairMaterials[(int)value];
        hairRenderers[2].materials = materialsArr;

        materialsArr = hairRenderers[3].materials;
        materialsArr[1] = hairMaterials[(int)value];
        materialsArr[2] = hairMaterials[(int)value];
        hairRenderers[3].materials = materialsArr;

        materialsArr = hairRenderers[4].materials;
        materialsArr[0] = hairMaterials[(int)value];
        hairRenderers[4].materials = materialsArr;
    }

    public void SlidePantsColor(float value)
    {
        for (int i = 0; i < pants.Count; i++)
        {
            Material[] materialsArr = pants[i].materials;
            materialsArr[0] = clothingMaterials[(int)value];
            pants[i].materials = materialsArr; 
        }
    }

    public void SlideShirtColor(float value)
    {
        for (int i = 0; i < shirts.Count; i++)
        {
            if (i == 0) 
            {
                Material[] materialsArr = shirts[i].materials;
                materialsArr[1] = clothingMaterials[(int)value];
                shirts[i].materials = materialsArr; 
            } else 
            {
                Material[] materialsArr = shirts[i].materials;
                materialsArr[0] = clothingMaterials[(int)value];
                shirts[i].materials = materialsArr; 
            }
            
        }
    }

    public void CreateCharacter()
    {
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (isMale)
        {
            maleModel.GetComponent<CharacterInput>().enabled = true;
            maleModel.GetComponent<PlayerControlScript>().enabled = true;
            SceneManager.MoveGameObjectToScene(maleModel, SceneManager.GetSceneByName(sceneName));
        } else 
        {
            femaleModel.GetComponent<CharacterInput>().enabled = true;
            femaleModel.GetComponent<PlayerControlScript>().enabled = true;
            SceneManager.MoveGameObjectToScene(femaleModel, SceneManager.GetSceneByName(sceneName));           
        }
        SceneManager.UnloadSceneAsync(currentScene);
        
       
    }
}
