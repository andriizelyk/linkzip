document.getElementById("shortBtn").onclick=async() => {
    await OnShortClick();
  };

document.getElementById('copyButton').onclick = () => {
    const shortLink = document.getElementById('shortLink').href;
    navigator.clipboard.writeText(shortLink).then(() => {
        document.getElementById('copyButton').textContent = 'Copied!';
        setTimeout(() => {
            document.getElementById('copyButton').textContent = 'Copy to Clipboard';
        }, 2000);
    }).catch(err => {
        console.error('Failed to copy: ', err);
    });
};

async function OnShortClick() {
    const url = document.getElementById('url').value;

    const response = await fetch(`/api/getshortlink?url=${url}&hours=24`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const result = await response.json();
    const shortLink = result.shortUrl;

    document.getElementById('shortLink').textContent = shortLink;
    document.getElementById('shortLink').href = shortLink;
    document.getElementById('result').style.display = 'block';
}